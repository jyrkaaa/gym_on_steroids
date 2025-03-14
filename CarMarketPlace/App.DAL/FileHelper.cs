namespace App.DAL;

public static class FileHelper
{
    public static string BasePath = Environment
                                        .GetFolderPath(System.Environment.SpecialFolder.UserProfile)
                                    + Path.DirectorySeparatorChar + "RiderProjects" + Path.DirectorySeparatorChar + "icd0008-24f" + Path.DirectorySeparatorChar + "CarMarketPlace_1" + Path.DirectorySeparatorChar + "WebApp";
}