namespace Project_IPET.Services
{
    public class CPostTools
    {

        public void Page(int countbypage, int totalpost, out int tatalpage )
        {

            if (countbypage == 0)
            {
                tatalpage = 1;

            }
            else
            {
                tatalpage = totalpost / countbypage;
            }
            if (totalpost % countbypage > 0)
            {
                tatalpage += 1;
            }

        }
    }
}
