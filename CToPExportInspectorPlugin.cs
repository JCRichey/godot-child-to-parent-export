#if TOOLS
using Godot;

/*
    Currently only supports a single child

    This was extremly painful to get working. Worth it? Yes. Simple & incomplete & prob has
    bugs I havent found yes BUT it works for what I made it for. I do plan to finish this 
    properly 
*/


public partial class CToPExportInspectorPlugin : EditorInspectorPlugin
{
    EditorInterface _EditorInterface;

    static string TargetUsageGroup = "ParentExport";

    public CToPExportInspectorPlugin() // Why does Godot freak out if we don't have this?
    {
    }
    public CToPExportInspectorPlugin(EditorInterface editorInterface)
    {
        _EditorInterface = editorInterface;
    }

    public override bool _CanHandle(GodotObject @object)
    {   
        if(!@object.IsClass("Node")){
            return false;
        }

        GD.Print("New Selection"); 
        GD.Print("_______________________________");
        
        return true;
    }
    
    public override void _ParseBegin(GodotObject @object)
    {
        // Do we have any nodes selected?
        var selectedNodesArray = _EditorInterface.GetSelection().GetSelectedNodes();
        if(selectedNodesArray == null || selectedNodesArray.Count < 1){
            return;
        }
        Node node = selectedNodesArray[0];

        // Does our first selected node have children?
        var nodeChildrenArray = node.GetChildren();
        if(nodeChildrenArray.Count < 1){
            return;
        }
        // Can we get the properties?
        var childPropertyArray = nodeChildrenArray[0].GetPropertyList();
        if(childPropertyArray.Count < 1){
            return;
        }
        //GD.Print(childPropertyArray);

        // Check all Properties to find any marked as TargetGroupGroup
        // They work kinda strangly. A property is put in a Group by having a
        // property above it with the Usage Hint PropertyUsageFlags.Group flag set
        for(int i = 0; i < childPropertyArray.Count; i++)
        {
            int PropertyUsageFlags = (int)childPropertyArray[i]["usage"];

            // Is the property a Group?
            if(!((PropertyUsageFlags & (int)Godot.PropertyUsageFlags.Group) > 0)){
                continue;
            }
            // Is it's name our TargetUsageGroup?
            if(childPropertyArray[i]["name"].ToString() != TargetUsageGroup){
                continue;
            }
            // If so, the next property is a target property of the child node
            var property = childPropertyArray[i + 1];

            DebugPrintProperties(nodeChildrenArray[0], property);

            Variant.Type type = (Variant.Type)((int)property["type"]);  
            GD.Print($"{node.Name}: {property["name"].ToString()}");  
            switch(type)
            {
                default:
                GD.PrintErr($"{(Variant.Type)type}: TYPE NOT IMPLEMENTED YET!");
                break;
                
                case Variant.Type.Bool:
                AddPropertyEditor(property["name"].ToString(), new BoolEditorProperty(nodeChildrenArray[0], property));
                break;

                case Variant.Type.String:
                AddPropertyEditor(property["name"].ToString(), new StringEditorProperty(nodeChildrenArray[0], property));
                break;
                //break;
            }

            
        }
        
        base._ParseBegin(@object);
    }


    void DebugPrintProperties(Node node, Godot.Collections.Dictionary prop)
    {
        string propName = prop["name"].ToString(); 
        GD.Print("_________________________________");
        GD.Print($"Property: {propName}");

        var value = node.Get(propName);
        GD.Print($"Value: {value}");
    }
}
#endif