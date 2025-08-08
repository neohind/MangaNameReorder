using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilenameReplace
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>     
        static void Main()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);

            RenameFiles(dirInfo);






        }

        static Regex m_regex = new Regex(@"(?<name>.+?)\-(?<number>\d+)(?<ext>\..+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        static void RenameFiles(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] childDirectories = dirInfo.GetDirectories();
            foreach (DirectoryInfo childDir in childDirectories)
            {
                // 재귀적으로 하위 디렉토리의 파일 이름을 변경합니다.
                RenameFiles(childDir);
            }

            // 디렉토리 내의 모든 파일을 가져옵니다.
            List<FileInfo> aryFileInfos = new List<FileInfo>();
            FileInfo[] files = dirInfo.GetFiles("*-2화*.zip");
            aryFileInfos.AddRange(files);

            foreach (FileInfo fileInfo in aryFileInfos)
            {
                string sFirstFilename = fileInfo.Name.Replace("-2화", "-1화");
                if (dirInfo.GetFiles(sFirstFilename).Length == 0)
                {
                    string sFirstFileOrigin = fileInfo.Name.Replace("-2화", "화");
                    FileInfo firstFileInfo = new FileInfo(Path.Combine(dirInfo.FullName, sFirstFileOrigin));
                    if (firstFileInfo.Exists)
                    {
                        // 첫 번째 화가 존재하는 경우, 두 번째 화를 삭제합니다.
                        firstFileInfo.MoveTo(Path.Combine(dirInfo.FullName, sFirstFilename));                        
                    }
                }
            }

        }
    }
}
