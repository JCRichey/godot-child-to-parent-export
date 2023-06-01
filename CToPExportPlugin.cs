#if TOOLS

using Godot;
using System;

[Tool]
public partial class CToPExportPlugin : EditorPlugin
{
	private CToPExportInspectorPlugin _plugin;

    public override void _EnterTree()
    {
        _plugin = new CToPExportInspectorPlugin(GetEditorInterface());

        AddInspectorPlugin(_plugin);
    }

    public override void _ExitTree()
    {
        RemoveInspectorPlugin(_plugin);
    }
}
#endif