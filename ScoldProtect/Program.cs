﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using ScoldProtect.Core;
using ScoldProtect.Core.Flow;
using ScoldProtect.Core.hideMethods;
using ScoldProtect.Core.Junk;
using ScoldProtect.Core.Numbers;
using ScoldProtect.Core.Proxy;
using ScoldProtect.Core.Rename;
using ScoldProtect.Core.SizeOFF;
using ScoldProtect.Core.String;

namespace ScoldProtect
{
    class Program
    {
        static void Main(string[] args)
        {
            ModuleDefMD module = ModuleDefMD.Load(args[0]);
            Junk.Run(module);
            Junk.JunkString(module);
            StringEnc2.Run(module);
            SizeOFF.Run(module);
            StringEnc.Run(module);
            ControlFlow.Execute(module);
            Junk.junkfield(module);
            Numbers.Run(module);
            Rename.Run(module);
            proxy.Run(module);
            hideMethods.Execute(module);
            var text2 = Path.GetDirectoryName(args[0]);
            if (text2 != null && !text2.EndsWith("\\"))
            { text2 += "\\"; }
            var path = text2 + Path.GetFileNameWithoutExtension(args[0]) + "_protected" +
                       Path.GetExtension(args[0]);
            module.Write(path, new ModuleWriterOptions(module)
            {
                PEHeadersOptions = { NumberOfRvaAndSizes = 13 },
                Logger = DummyLogger.NoThrowInstance
            });
        }
	}  
}
