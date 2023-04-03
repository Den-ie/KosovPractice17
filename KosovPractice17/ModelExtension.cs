using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosovPractice17
{
    public partial class TeamEntities
    {
        private static TeamEntities context;

        public static TeamEntities GetContext()
        {
            if(context == null)
                context = new TeamEntities();
            return context;
        }
    }
}
