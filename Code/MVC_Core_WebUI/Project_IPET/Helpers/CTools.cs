using Project_IPET.Models.EF;

namespace Project_IPET.Services
{
    public class CTools
    {
        
        public void Page(int pagesize, int totalpost, out int tatalpage)
        {

            if (pagesize == 0)
            {
                tatalpage = 1;

            }
            else
            {
                tatalpage = totalpost / pagesize;
            }
            if (totalpost % pagesize > 0)
            {
                tatalpage += 1;
            }

        }

    }
}
