using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts
{
    public class HierarchyHighlighter : MonoBehaviour
    {
        public static readonly Color DEFAULT_BACKGROUND_COLOR = new Color(0.76f, 0.76f, 0.76f, 1f);

        public static readonly Color DEFAULT_TEXT_COLOR = Color.black;


        public Color Text_Color = DEFAULT_TEXT_COLOR;
        public FontStyle TextStyle = FontStyle.Normal;


        public bool Highlight_BackgroundColor = false;
        public Color Background_Color = DEFAULT_BACKGROUND_COLOR;
    }
}
