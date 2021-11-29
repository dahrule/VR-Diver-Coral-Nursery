using UnityEngine;

//Works with TagSelectorPropertyDrawer script to show gametags in the Editor as Serializable fields. Its recommended that this script is next to the Editor folder containing the TagSelectorPropertyDrawer.
public class TagSelectorAttribute : PropertyAttribute
{
    public bool UseDefaultTagFieldDrawer = false;
}