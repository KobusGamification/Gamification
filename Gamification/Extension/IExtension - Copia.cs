﻿dusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public interface IExtension
    {
        IExtension Merge(IExtension extension);
    }
}
