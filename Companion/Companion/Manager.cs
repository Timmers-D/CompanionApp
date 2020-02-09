using Companion.Companion;
using Companion.Struct;
using Companion.Visual;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Companion
{
    class Manager
    {
        // list of companions
        private List<CompanionBase> _companions = new List<CompanionBase>();

        // companion updates, handled by the respective companions
        public bool Update()
        {
            if (_companions.Count == 0) { return false; }

            foreach (var companion in _companions)
            {
                companion.Update();
            }

            return true;
        }

        // get the image to draw from each companion and then sending it to get drawn
        public void DrawCompanions(IntPtr hWnd)
        {
            var bitLocs = new List<BitmapLocation>();
            foreach (var companion in _companions)
            {
                bitLocs.Add(companion.GetCurrentBody());
                bitLocs.Add(companion.GetCurrentFace());
            }
            Drawing.DrawBitmaps(hWnd, bitLocs);
        }

        public void AddCompanion(CompanionBase companion)
        {
            _companions.Add(companion);
        }

        public int GetCompanionCount()
        {
            return _companions.Count;
        }

        // hasta la vista companion
        public void DisposeCompanion(CompanionBase companion)
        {
            _companions.Remove(companion);
        }
    }
}
