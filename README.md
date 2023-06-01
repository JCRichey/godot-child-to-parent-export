# godot-child-to-parent-export
## Godot 4 Mono Plugin to allow changing Child Export Varibles from the Parent
Add the folder to your addons, build, then reload the engine and active the plugin.
To enable a Child's Export Varibles to be seen, add ```[ExportGroup("ParentExport")]``` above them.
They will then show up on the Parent's inspector. Curently only bool & string types are implemented. 
I will be working on fully completing this, but for now it works for what I built it for. 
I'm open to any feedback & contributions, you can reach me at jcrichey@proton.me 
