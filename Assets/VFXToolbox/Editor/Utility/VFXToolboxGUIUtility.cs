using UnityEngine;
using System.Collections.Generic;

namespace UnityEditor.VFXToolbox
{
    class VFXToolboxGUIUtility
    {
        #region GUIContent caching
        private static Dictionary<string, GUIContent> s_GUIContentCache;

        public static GUIContent Get(string textAndTooltip)
        {
            return GetTextAndIcon(textAndTooltip, null);
        }

        public static GUIContent GetTextAndIcon(string textAndTooltip, string icon)
        {
            if (s_GUIContentCache == null)
                s_GUIContentCache = new Dictionary<string, GUIContent>();

            if (string.IsNullOrEmpty(textAndTooltip))
                return GUIContent.none;

            GUIContent content;

            if (!s_GUIContentCache.TryGetValue(textAndTooltip, out content))
            {
                var s = textAndTooltip.Split('|');

                if (!string.IsNullOrEmpty(icon))
                {
                    var iconContent = EditorGUIUtility.IconContent(icon);
                    content = new GUIContent(s[0], iconContent.image);
                }
                else
                {
                    content = new GUIContent(s[0]);
                }

                if (s.Length > 1 && !string.IsNullOrEmpty(s[1]))
                    content.tooltip = s[1];

                s_GUIContentCache.Add(textAndTooltip, content);
            }

            return content;

        }

        public static void Clear()
        {
            s_GUIContentCache.Clear();
        }
        #endregion

        #region ProgressBar Handling

        private static double s_LastProgressBarTime;

        /// <summary>
        /// Displays a progress bar with delay and optional cancel button
        /// </summary>
        /// <param name="title">title of the window</param>
        /// <param name="message">message</param>
        /// <param name="progress">progress</param>
        /// <param name="delay">minimum delay before displaying window</param>
        /// <param name="cancelable">is the window cancellable?</param>
        /// <returns>true if cancelled, false otherwise</returns>
        public static bool DisplayProgressBar(string title, string message, float progress, float delay = 0.0f, bool cancelable = false)
        {
            if(s_LastProgressBarTime < 0.0)
                s_LastProgressBarTime = EditorApplication.timeSinceStartup;

            if (EditorApplication.timeSinceStartup - s_LastProgressBarTime > delay)
            {
                if(cancelable)
                {
                    return EditorUtility.DisplayCancelableProgressBar(title, message, progress);
                }
                else
                {
                    EditorUtility.DisplayProgressBar(title, message, progress);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Clears the current progressbar
        /// </summary>
        public static void ClearProgressBar()
        {
            s_LastProgressBarTime = -1.0;
            EditorUtility.ClearProgressBar();
        }

        #endregion

        #region Other GUI Utils
        public static void GUIRotatedLabel(Rect position, string label, float angle, GUIStyle style)
        {
            var matrix = GUI.matrix;
            var rect = new Rect(position.x - 10f, position.y, position.width, position.height);
            GUIUtility.RotateAroundPivot(angle, rect.center);
            GUI.Label(rect, label, style);
            GUI.matrix = matrix;
        }
        #endregion

    }
}