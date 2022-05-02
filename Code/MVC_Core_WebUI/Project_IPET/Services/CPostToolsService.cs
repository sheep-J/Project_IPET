namespace Project_IPET.Services
{
    public class CPostToolsService
    {

        public void Page(int count,int totalpost, out int tatalpage )
        {

           

            if (count == 0)
            {
                tatalpage = 1;

            }
            else
            {
                tatalpage = totalpost / count;
            }
            if (totalpost % count > 0)
            {
                tatalpage += 1;
            }

        }
    }
}
