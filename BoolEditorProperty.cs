#if TOOLS
using Godot;

/*
    Why am I not using this in it's intended way? Because this hacky way
    requires less boiler plate & does exactly what I need

    Somebody can fix this to be proper if they want :P
*/

public partial class BoolEditorProperty : EditorProperty
{
    HBoxContainer HBox    = new HBoxContainer();
    CheckBox      ValueCB = new CheckBox();

    Node Node; 
    Godot.Collections.Dictionary Property;


    bool Value
    {
        get
        {
            if(Property == null || Node == null){
                GD.PrintErr("BoolEditorProperty: Property or Node null. I have no suggestions ;(");
                return false;
            }
            // Fetch Value from node
            return (bool)Node.Get(Property["name"].ToString());
        }
        set
        {
            if(Property == null || Node == null){
                GD.PrintErr("BoolEditorProperty: Property or Node null. I have no suggestions ;(");
            }

            // Set value in node
            Node.Set(Property["name"].ToString(), value);
        }
    }

    public BoolEditorProperty(Node node, Godot.Collections.Dictionary property)
    {
        Node = node; Property = property;
        
        Label = property["name"].ToString();

        AddChild(HBox);
        HBox.AddChild(ValueCB);
        
        ValueCB.Connect("toggled", new Callable(this, "CheckBoxToggle"));

        // Setup Name label & Set value of checkbox
        ValueCB.ButtonPressed = Value;
        GD.Print($"BOOL Property '{node.Name}': {property["name"].ToString()}");
        
        // Make sure the control is able to retain the focus.
        AddFocusable(HBox);
        AddFocusable(ValueCB);
    }

    public void CheckBoxToggle(bool button_pressed)
    {
        Value = button_pressed;
    }
}
#endif