using System;
using System.Collections.Generic;
using System.Linq;

class Controls
{
    private List<Control> _controls  = new List<Control>();

     public void Add(Control control)
    {
        _controls.Add(control);
    }

    public void Remove(Control control)
    {
        _controls.Remove(control);
    }

}

