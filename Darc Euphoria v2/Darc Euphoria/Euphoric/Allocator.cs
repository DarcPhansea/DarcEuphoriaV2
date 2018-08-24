using System;
using System.Collections.Generic;
using System.Linq;
using Darc_Euphoria.Euphoric.Enums;

namespace Darc_Euphoria.Euphoric
{
    public class Allocator
    {
        public Dictionary<IntPtr, IntPtr> AllocatedSize = new Dictionary<IntPtr, IntPtr>();

        public IntPtr AlloacNewPage(IntPtr size)
        {
            var Address = WinAPI.VirtualAllocEx(Memory.pHandle, IntPtr.Zero, (IntPtr) 4096,
                (int) FreeType.MEM_COMMIT | (int) FreeType.MEM_RESERVE, WinAPI.PAGE_READWRITE);

            AllocatedSize.Add(Address, size);

            return Address;
        }

        public void Free()
        {
            foreach (var key in AllocatedSize)
                WinAPI.VirtualFreeEx(Memory.pHandle, key.Key, 4096,
                    (int) FreeType.MEM_COMMIT | (int) FreeType.MEM_RESERVE);
        }

        public IntPtr Alloc(int size)
        {
            for (var i = 0; i < AllocatedSize.Count; ++i)
            {
                var key = AllocatedSize.ElementAt(i).Key;
                var value = (int) AllocatedSize[key] + size;
                if (value < 4096)
                {
                    var CurrentAddres = IntPtr.Add(key, (int) AllocatedSize[key]);
                    AllocatedSize[key] = new IntPtr(value);
                    return CurrentAddres;
                }
            }

            return AlloacNewPage(new IntPtr(size));
        }
    }
}