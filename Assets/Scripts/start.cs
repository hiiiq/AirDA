using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class start : MonoBehaviour
{
    public GameObject tableprefab;
    private GameObject tablespawner;
    private GameObject cellspawner;
    public GameObject resizer;
    private string[,] matrix;
    private string[,] matrix1;
    private string[,] matrix2;
    public GameObject[] tables;
    public Vector3[] grid;
    public float[] scales;
    public static bool inspector = false;
    public static int? selected;

    void Start()
    {
        tables = new GameObject[3];
        grid = new Vector3[3];
        scales = new float[3];
        //startmatrices();
      
        tablespawner = Instantiate(tableprefab);
        tablespawner.GetComponent<table>().data = dbhandler.table;
        tablespawner.GetComponent<table>().tableid = 0;
        tablespawner.transform.localPosition = new Vector3(
            transform.localPosition.x, transform.localPosition.y + 7f - 7f * 0,
            transform.localPosition.z - 0.22f);
        tables[0] = tablespawner;
        scales[0] = tablespawner.transform.localScale.x;

        tablespawner = Instantiate(tableprefab);
        tablespawner.GetComponent<table>().data = dbhandler.table1;
        tablespawner.GetComponent<table>().tableid = 1;
        tablespawner.transform.localPosition = new Vector3(
            transform.localPosition.x, transform.localPosition.y + 7f - 7f * 1,
            transform.localPosition.z - 0.22f);
        tables[1] = tablespawner;
        scales[1] = tablespawner.transform.localScale.x;

        tablespawner = Instantiate(tableprefab);
        tablespawner.GetComponent<table>().data = dbhandler.table1;
        tablespawner.GetComponent<table>().tableid = 2;
        tablespawner.transform.localPosition = new Vector3(
            transform.localPosition.x, transform.localPosition.y + 7f - 7f * 2,
            transform.localPosition.z - 0.22f);
        tables[2] = tablespawner;
        scales[2] = tablespawner.transform.localScale.x;

    }
    
    void Update () {
        if (selected != null)
        {
            tables[(int) selected].transform.position = new Vector3(
                resizer.transform.localPosition.x + 15f, resizer.transform.localPosition.y,
                resizer.transform.localPosition.z - 1f);
            tables[(int) selected].transform.localScale = new Vector3(3f, 3f, 1f);
            tables[(int) selected].transform.GetComponent<BoxCollider>().size = new Vector3(8f, 6.2f, 0.1f);
            tables[(int) selected].transform.GetComponentInChildren<BoxCollider>().enabled = false;
            for (int i = 0; i < tables.Length; i++)
            {
                if (i != selected)
                {
                    tables[i].transform.position = new Vector3(
                        resizer.transform.localPosition.x - 2.8f, resizer.transform.localPosition.y + 7f - 7f * i,
                        resizer.transform.localPosition.z - 0.22f);
                    grid[i] = tables[i].transform.position;
                    tables[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    tables[i].transform.GetComponent<BoxCollider>().size = new Vector3(8f, 6.2f, 2f);
                    tables[i].transform.GetComponentInChildren<BoxCollider>().enabled = true;
                }

            }
        }
        else
        {
            for (int i = 0; i < tables.Length; i++)
            {
                if (i != selected)
                {
                    tables[i].transform.position = new Vector3(
                        resizer.transform.localPosition.x - 2.8f, resizer.transform.localPosition.y + 7f - 7f * i,
                        resizer.transform.localPosition.z - 0.22f);
                    grid[i] = tables[i].transform.position;
                    tables[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    tables[i].transform.GetComponent<BoxCollider>().size = new Vector3(8f, 6.2f, 2f);
                    tables[i].transform.GetComponentInChildren<BoxCollider>().enabled = true;
                }

            }
        }
        if (!inspector)
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].transform.position = new Vector3(
                    resizer.transform.localPosition.x - 2.8f, resizer.transform.localPosition.y + 7f - 7f * i,
                    resizer.transform.localPosition.z - 0.22f);
                grid[i] = tables[i].transform.position;
                tables[i].transform.localScale = new Vector3(1f, 1f, 1f);
                tables[i].transform.GetComponent<BoxCollider>().size = new Vector3(8f, 6.2f, 2f);
                tables[i].transform.GetComponentInChildren<BoxCollider>().enabled = true;
            }
        }
    }

    public static void Inspect(int tableid)
    {
        selected = tableid;
    }

    public void CreateNewTable(string[,] data)
    {
        tablespawner = Instantiate(tableprefab);
        tablespawner.GetComponent<table>().data = data;
        tablespawner.GetComponent<table>().movingtable = true;
        tablespawner.GetComponent<BoxCollider>().enabled = true;
        tablespawner.GetComponent<table>().sheet.GetComponent<BoxCollider>().enabled = false;
        tablespawner.GetComponent<BoxCollider>().size = new Vector3(data.GetLength(1) + 1.75f, 6.5f, 1f);
    }

    void startmatrices()
    {
        matrix = new string[,]
       {   {"id","first.n","last.n","email","gender","ip.adres","color"},
            {"1","Angela","Morris","amorris0@blogger.com","Female","29.208.104.119","Khaki"},
            {"2","Kevin","Perry","kperry1@ask.com","Male","136.148.238.248","Purple"},
            {"3","Jimmy","Hayes","jhayes2@bbc.co.uk","Male","39.200.222.75","Mauv"},
            {"4","Mary","Gonzalez","mgonzalez3@merriam-webster.com","Female","195.12.86.167","Violet"},
            {"5","Kathleen","Hall","khall4@clickbank.net","Female","72.173.223.111","Fuscia"},
            {"6","Betty","Turner","bturner5@tmall.com","Female","248.105.47.39","Mauv"},
            {"7","Brandon","Griffin","bgriffin6@illinois.edu","Male","238.218.61.4","Mauv"},
            {"8","Annie","Fuller","afuller7@jiathis.com","Female","120.40.178.27","Puce"},
            {"9","Cynthia","Ryan","cryan8@admin.ch","Female","153.231.227.39","Khaki"},
            {"10","Lillian","Romero","lromero9@joomla.org","Female","65.129.97.65","Red"},
            {"11","Brenda","Collins","bcollinsa@unblog.fr","Female","97.255.220.61","Green"},
            {"12","Kathy","Pierce","kpierceb@odnoklassniki.ru","Female","191.169.19.169","Blue"},
            {"13","Bonnie","Schmidt","bschmidtc@soundcloud.com","Female","163.253.91.95","Mauv"},
            {"14","Janet","Reid","jreidd@telegraph.co.uk","Female","85.73.36.85","Violet"},
            {"15","Jonathan","Ramirez","jramireze@icq.com","Male","17.53.114.86","Goldenrod"},
            {"16","Randy","Austin","raustinf@slate.com","Male","251.146.77.156","Goldenrod"},
            {"17","Shawn","Butler","sbutlerg@myspace.com","Male","212.186.1.76","Mauv"},
            {"18","Susan","Thompson","sthompsonh@redcross.org","Female","185.197.158.229","Goldenrod"},
            {"19","Alan","Scott","ascotti@com.com","Male","200.218.253.36","Indigo"},
            {"20","Timothy","Mendoza","tmendozaj@google.ru","Male","81.40.6.250","Aquamarine"},
            {"21","Terry","Cox","tcoxk@home.pl","Male","15.65.51.184","Khaki"},
            {"22","Lori","Chapman","lchapmanl@myspace.com","Female","204.4.113.167","Violet"},
            {"23","Robin","Perry","rperrym@state.tx.us","Female","161.248.227.120","Khaki"},
            {"24","Mildred","Payne","mpaynen@ifeng.com","Female","254.198.155.119","Puce"},
            {"25","Randy","Duncan","rduncano@topsy.com","Male","190.169.231.91","Maroon"},
            {"26","Norma","Lopez","nlopezp@edublogs.org","Female","28.69.229.58","Yellow"},
            {"27","Steve","Stewart","sstewartq@mit.edu","Male","96.91.68.245","Yellow"},
            {"28","Christine","Hamilton","chamiltonr@joomla.org","Female","173.63.56.237","Goldenrod"},
            {"29","Christopher","Bradley","cbradleys@technorati.com","Male","96.235.150.169","Goldenrod"},
            {"30","Billy","Hanson","bhansont@so-net.ne.jp","Male","77.126.233.43","Maroon"},
            {"31","Jack","Arnold","jarnoldu@ftc.gov","Male","211.236.192.147","Teal"},
            {"32","Catherine","Torres","ctorresv@123-reg.co.uk","Female","86.99.225.118","Crimson"},
            {"33","Betty","Gonzalez","bgonzalezw@booking.com","Female","192.17.181.147","Red"},
            {"34","Amanda","Freeman","afreemanx@ebay.co.uk","Female","25.135.190.159","Puce"},
            {"35","Patricia","Rivera","priveray@omniture.com","Female","138.93.208.231","Crimson"},
            {"36","Jane","Carter","jcarterz@mayoclinic.com","Female","193.115.86.219","Violet"},
            {"37","Kathleen","Duncan","kduncan10@stanford.edu","Female","237.100.127.73","Red"},
            {"38","Jonathan","Holmes","jholmes11@chron.com","Male","126.116.235.7","Fuscia"},
            {"39","Debra","Elliott","delliott12@naver.com","Female","173.169.103.42","Indigo"},
            {"40","Anna","Dixon","adixon13@ihg.com","Female","231.152.74.210","Mauv"},
            {"41","Sara","Gutierrez","sgutierrez14@salon.com","Female","165.106.255.120","Puce"},
            {"42","Janet","Fernandez","jfernandez15@vkontakte.ru","Female","251.43.226.69","Teal"},
            {"43","Jonathan","Price","jprice16@twitpic.com","Male","191.111.142.5","Teal"},
            {"44","Chris","Ramos","cramos17@bloglines.com","Male","13.140.3.219","Aquamarine"},
            {"45","David","Chavez","dchavez18@state.gov","Male","41.123.201.147","Green"},
            {"46","Craig","Long","clong19@networksolutions.com","Male","211.62.184.120","Aquamarine"},
            {"47","Steven","Hudson","shudson1a@seesaa.net","Male","141.250.101.105","Goldenrod"},
            {"48","Jason","Jackson","jjackson1b@myspace.com","Male","236.147.161.251","Purple"},
            {"49","Ralph","Miller","rmiller1c@dagondesign.com","Male","52.198.82.64","Violet"},
            {"50","Chris","Perry","cperry1d@army.mil","Male","229.61.177.201","Purple"},
            {"51","Adam","Wagner","awagner1e@opensource.org","Male","75.190.48.166","Puce"},
            {"52","Melissa","Lawson","mlawson1f@senate.gov","Female","196.94.213.203","Yellow"},
            {"53","Adam","Fernandez","afernandez1g@amazon.de","Male","28.175.196.103","Green"},
            {"54","Stephanie","Reyes","sreyes1h@flickr.com","Female","158.19.160.231","Orange"},
            {"55","James","Gutierrez","jgutierrez1i@state.gov","Male","248.228.142.240","Pink"},
            {"56","Carlos","Harvey","charvey1j@amazon.co.uk","Male","183.161.129.50","Khaki"},
            {"57","Pamela","Franklin","pfranklin1k@home.pl","Female","139.251.40.14","Purple"},
            {"58","Diana","Hart","dhart1l@indiatimes.com","Female","216.138.120.198","Violet"},
            {"59","Chris","Morgan","cmorgan1m@google.com","Male","207.168.147.157","Purple"},
            {"60","Howard","Powell","hpowell1n@accuweather.com","Male","202.225.53.228","Blue"},
            {"61","Mildred","King","mking1o@addthis.com","Female","107.165.65.60","Purple"},
            {"62","Lawrence","Taylor","ltaylor1p@sina.com.cn","Male","44.212.78.122","Red"},
            {"63","Louis","Dixon","ldixon1q@netvibes.com","Male","67.117.77.125","Purple"},
            {"64","Jesse","Coleman","jcoleman1r@yale.edu","Male","2.224.194.145","Puce"},
            {"65","Robert","Wallace","rwallace1s@opera.com","Male","164.75.53.123","Khaki"},
            {"66","Dorothy","Collins","dcollins1t@gmpg.org","Female","216.173.221.108","Purple"},
            {"67","Phyllis","Garcia","pgarcia1u@walmart.com","Female","175.65.139.215","Orange"},
            {"68","George","Shaw","gshaw1v@dyndns.org","Male","150.96.141.23","Yellow"},
            {"69","Lillian","Taylor","ltaylor1w@netlog.com","Female","198.167.26.212","Mauv"},
            {"70","Teresa","Stevens","tstevens1x@trellian.com","Female","57.82.47.230","Mauv"},
            {"71","Stephen","Carroll","scarroll1y@umn.edu","Male","254.62.56.107","Goldenrod"},
            {"72","Anthony","Cole","acole1z@edublogs.org","Male","162.64.182.128","Purple"},
            {"73","Robert","Coleman","rcoleman20@webeden.co.uk","Male","181.208.222.4","Orange"},
            {"74","Carlos","Washington","cwashington21@wunderground.com","Male","179.78.184.162","Teal"},
            {"75","Judith","Frazier","jfrazier22@youku.com","Female","253.26.26.234","Aquamarine"},
            {"76","Ernest","Sullivan","esullivan23@unblog.fr","Male","194.113.158.68","Red"},
            {"77","Anthony","Lynch","alynch24@statcounter.com","Male","82.121.139.83","Pink"},
            {"78","Lori","Gray","lgray25@rambler.ru","Female","22.30.134.178","Teal"},
            {"79","Ruth","Dixon","rdixon26@narod.ru","Female","57.159.72.73","Fuscia"},
            {"80","Craig","Ward","cward27@google.it","Male","72.5.217.93","Violet"},
            {"81","Melissa","Simpson","msimpson28@upenn.edu","Female","100.193.42.228","Mauv"},
            {"82","Michael","Wood","mwood29@ustream.tv","Male","86.89.188.81","Blue"},
            {"83","Julie","Ortiz","jortiz2a@ning.com","Female","45.26.30.127","Yellow"},
            {"84","Marie","Miller","mmiller2b@nationalgeographic.com","Female","129.79.248.146","Green"},
            {"85","Amanda","Garrett","agarrett2c@wikimedia.org","Female","62.228.7.49","Orange"},
            {"86","Laura","Patterson","lpatterson2d@delicious.com","Female","247.229.223.221","Crimson"},
            {"87","Fred","Dunn","fdunn2e@webs.com","Male","216.215.188.185","Pink"},
            {"88","Michelle","Mitchell","mmitchell2f@va.gov","Female","33.236.36.171","Aquamarine"},
            {"89","Arthur","Rodriguez","arodriguez2g@mapy.cz","Male","201.45.202.230","Yellow"},
            {"90","Justin","Taylor","jtaylor2h@jalbum.net","Male","136.213.255.242","Mauv"},
            {"91","Judy","Franklin","jfranklin2i@yellowpages.com","Female","4.163.29.65","Khaki"},
            {"92","Marie","Ward","mward2j@ameblo.jp","Female","209.69.217.27","Red"},
            {"93","Todd","Reyes","treyes2k@php.net","Male","138.219.156.208","Pink"},
            {"94","Charles","Meyer","cmeyer2l@quantcast.com","Male","86.83.127.130","Fuscia"},
            {"95","Amy","White","awhite2m@gravatar.com","Female","88.102.146.244","Orange"},
            {"96","Marie","Parker","mparker2n@usgs.gov","Female","25.235.232.20","Turquoise"},
            {"97","Brian","Robinson","brobinson2o@hexun.com","Male","101.105.113.166","Green"},
            {"98","Ruby","Bishop","rbishop2p@i2i.jp","Female","74.112.240.131","Goldenrod"},
            {"99","Janet","Russell","jrussell2q@surveymonkey.com","Female","218.199.83.249","Goldenrod"},
            {"100","Dennis","Morales","dmorales2r@people.com.cn","Male","204.203.244.185","Pink"}
       };

        matrix1 = new string[,]
        {
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"},
            {"0","1","2"}
        };

        matrix2 = new string[,]
        {
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"},
            {"0","1","2","3","4"}
        };
    }

}
