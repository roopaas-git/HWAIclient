﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerUtility
{
    public interface ICustomLogger
    {
        void Debug(string message);
        void Error(string message);
        void Information(string message);
        void Warning(string message);
    }
}
