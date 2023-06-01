#if TOOLS
using Godot;
using System;

public partial class StringEditorProperty : EditorProperty
{
    HBoxContainer HBox          = new HBoxContainer();
    LineEdit      ValueTextEdit = new LineEdit     ();

    Node Node; 
    Godot.Collections.Dictionary Property;


    string Value
    {
        get
        {
            if(Property == null || Node == null){
                GD.PrintErr("BoolEditorProperty: Property or Node null. I have no suggestions ;(");
                return null;
            }
            // Fetch Value from node
            return Node.Get(Property["name"].ToString()).ToString();
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

    public StringEditorProperty(Node node, Godot.Collections.Dictionary property)
    {
        Node = node; Property = property;
        
        Label = property["name"].ToString();

        AddChild(HBox);
        HBox.AddChild(ValueTextEdit);
        
        ValueTextEdit.Connect("text_changed", new Callable(this, "TextChanged"));

        // Setup Name label & Set value of checkbox
        ValueTextEdit.Text = Value;
        GD.Print($"String Property '{node.Name}': {property["name"].ToString()}");
        
        // Make sure the control is able to retain the focus.
        AddFocusable(HBox);
        AddFocusable(ValueTextEdit);
    }

    public void TextChanged(string new_text)
    {
        Value = new_text;
    }
}
#endif
