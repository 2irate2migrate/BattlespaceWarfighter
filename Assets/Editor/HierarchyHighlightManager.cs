using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEditor;

using Assets.Scripts;

namespace Assets.Editor
{
    [InitializeOnLoad]
    public class HierarchyHighlightManager
    {
        public static readonly Color DEFAULT_COLOR_HIERARCHY_SELECTED = new Color(0.243f, 0.4901f, 0.9058f, 1f);



        static HierarchyHighlightManager() { EditorApplication.hierarchyWindowItemOnGUI -= HierarchyHighlight_OnGUI; EditorApplication.hierarchyWindowItemOnGUI += HierarchyHighlight_OnGUI; }



        private static void HierarchyHighlight_OnGUI(int inSelectionID, Rect inSelectionRect)
        {
            GameObject GO_Label = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

            if (GO_Label != null)
            {
                HierarchyHighlighter Label = GO_Label.GetComponent<HierarchyHighlighter>();

                if(Label != null && Event.current.type == EventType.Repaint)
                {
                    if (Label.Highlight_BackgroundColor)
                    {
                        bool ObjectIsSelected = Selection.instanceIDs.Contains(inSelectionID);

                        Rect Offset = new Rect(inSelectionRect.position + new Vector2(2f, 0f), inSelectionRect.size);
                        Rect BackgroundOffset = new Rect(inSelectionRect.position, inSelectionRect.size);

                        //If the background has transparency, draw a solid color first
                        if(Label.Background_Color.a < 1f || ObjectIsSelected)
                        {
                            EditorGUI.DrawRect(BackgroundOffset, HierarchyHighlighter.DEFAULT_BACKGROUND_COLOR);
                        }

                        //Draw background
                        if (ObjectIsSelected)
                        {
                            EditorGUI.DrawRect(BackgroundOffset, Color.Lerp(GUI.skin.settings.selectionColor, Label.Background_Color, 0.1f));
                        }
                        else
                        {
                            EditorGUI.DrawRect(BackgroundOffset, Label.Background_Color);
                        }
                        
                        EditorGUI.LabelField(Offset, GO_Label.name, new GUIStyle()
                        {
                            normal = new GUIStyleState() { textColor = Label.Text_Color },
                            fontStyle = Label.TextStyle
                        });

                        EditorApplication.RepaintHierarchyWindow();
                    }
                }
            }
        }
    }
}