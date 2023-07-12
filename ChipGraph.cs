using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class ChipGraph
    {
        private readonly IReadOnlyList<ColorChip> colorChips;

        public ChipGraph(IReadOnlyList<ColorChip> colorChips)
        {
            this.colorChips = colorChips;
        }

        
    }
}
