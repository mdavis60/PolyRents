using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model.collections
{
    public class Resources : ObservableCollection<Resource>
    {
        public ObservableCollection<Resource> ResourceList { get; set; }

        public Resources()
        {
            ResourceList = new ObservableCollection<Resource>();
        }
    }
}
