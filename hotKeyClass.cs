using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRSWordAddIn
{
    
    public class hotKeyClass
    {
        private static IKeyboardMouseEvents m_Events;
        public static void Start()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
        }
        public static void Stop()
        {
            Unsubscribe();
        }
        private static void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;

            m_Events.MouseDragStarted += OnMouseDragStarted;
            m_Events.MouseDragFinished += OnMouseDragFinished;

        }

        private static void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;

            m_Events.MouseDragStarted -= OnMouseDragStarted;
            m_Events.MouseDragFinished -= OnMouseDragFinished;


            m_Events.Dispose();
            m_Events = null;
        }
        private static void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                //Log(string.Format("MouseDown \t\t {0}\n", e.Button));
                return;
            }

            //Log(string.Format("MouseDown \t\t {0} Suppressed\n", e.Button));
            e.Handled = true;
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Log(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));
        }

        private static void OnKeyUp(object sender, KeyEventArgs e)
        {
            //Log(string.Format("KeyUp  \t\t {0}\n", e.KeyCode));
        }

        private static void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Log(string.Format("KeyPress \t\t {0}\n", e.KeyChar));
        }

        private static void OnMouseDown(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseDown \t\t {0}\n", e.Button));
        }

        private static void OnMouseUp(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseUp \t\t {0}\n", e.Button));
        }

        private static void OnMouseClick(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseDoubleClick \t\t {0}\n", e.Button));
        }

        private static void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
           // Log("MouseDragStarted\n");
        }

        private static void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            //Log("MouseDragFinished\n");
        }

        private static void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            //labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }

        private static void HookManager_MouseWheelExt(object sender, MouseEventExtArgs e)
        {
            //labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
            //Log("Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }
    }

}
