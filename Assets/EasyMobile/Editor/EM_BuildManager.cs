#if UNITY_ANDROID || UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace EasyMobile.Editor
{
    #if UNITY_2018_1_OR_NEWER
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;

    public class EM_PreBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            EM_BuildProcessorUtil.PreBuildProcessing(report.summary.platform, report.summary.outputPath);
        }
    }

    public class EM_PostBuildProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 9999; } }

        public void OnPostprocessBuild(BuildReport report)
        {
            EM_BuildProcessorUtil.PostBuildProcessing(report.summary.platform, report.summary.outputPath);
        }
    }


#elif UNITY_5_6_OR_NEWER
    using UnityEditor.Build;

    public class EM_PreBuildProcessor : IPreprocessBuild
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
        EM_BuildProcessorUtil.PreBuildProcessing(target, path);
        }
    }

    public class EM_PostBuildProcessor : IPostprocessBuild
    {
        public int callbackOrder { get { return 9999; } }

        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            EM_BuildProcessorUtil.PostBuildProcessing(target, path);
        }
    }


#else
    using UnityEditor.Callbacks;

    public class EM_LegacyBuildProcessor
    {
        [PostProcessBuildAttribute(9999)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            EM_BuildProcessorUtil.PostBuildProcessing(target, path);
        }
    }
    #endif


    public class EM_BuildProcessorUtil
    {
        public static void PreBuildProcessing(BuildTarget target, string path)
        {
            // Perform pre-build processing here if needed.
        }

        public static void PostBuildProcessing(BuildTarget target, string path)
        {
#if UNITY_IOS
            if (target == BuildTarget.iOS)
            {
                // Read.
                string pbxPath = PBXProject.GetPBXProjectPath(path);
                PBXProject project = new PBXProject();
                project.ReadFromString(File.ReadAllText(pbxPath));

                string targetName = PBXProject.GetUnityTargetName();
                string targetGUID = project.TargetGuidByName(targetName);

                // Add frameworks if needed.

                // Add required flags.
                project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");

                // Write.
                File.WriteAllText(pbxPath, project.WriteToString());
            }
#endif
        }
    }
}
#endif