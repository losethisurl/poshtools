﻿using System;
using System.IO;
using Microsoft.VisualStudioTools.Project;
using PowerShellTools.LanguageService;

namespace PowerShellTools.Project
{
    internal class PowerShellProjectNode : CommonProjectNode
    {
        private readonly CommonProjectPackage _package;

        public PowerShellProjectNode(CommonProjectPackage package)
            : base(package, Utilities.GetImageList(typeof(PowerShellProjectNode).Assembly.GetManifestResourceStream("PowerShellTools.Project.Resources.ProjectIcon.bmp")))
        {
            _package = package;
            AddCATIDMapping(typeof (PowerShellModulePropertyPage), typeof (PowerShellModulePropertyPage).GUID);
        }

        public override Type GetProjectFactoryType()
        {
            return typeof (PowerShellProjectFactory);
        }

        public override Type GetEditorFactoryType()
        {
            return typeof(PowerShellEditorFactory);
        }

        public override string GetProjectName()
        {
            return "PowerShellProject";
        }

        public override string GetFormatList()
        {
            return "PowerShell Project File (*.pssproj)\n*.pssproj\nAll Files (*.*)\n*.*\n";
        }

        public override Type GetGeneralPropertyPageType()
        {
            return typeof (PowerShellGeneralPropertyPage);
        }

        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            return new[] { typeof(PowerShellGeneralPropertyPage).GUID, typeof(PowerShellModulePropertyPage).GUID };
        }

        public override Type GetLibraryManagerType()
        {
            return typeof(PowerShellLibraryManager);
        }

        public override IProjectLauncher GetLauncher()
        {
            return new PowerShellProjectLauncher();
        }

        protected override Stream ProjectIconsImageStripStream
        {
            get
            {
                return typeof(ProjectNode).Assembly.GetManifestResourceStream("PowerShellTools.Project.Resources.imagelis.bmp");
            }
        }

        public override int ImageIndex
        {
            get { return 52; } //TODO: Fix image index
        }

        public override string[] CodeFileExtensions
        {
            get
            {
                return new[] { PowerShellConstants.PS1File, PowerShellConstants.PSD1File, PowerShellConstants.PSM1File };
            }
        }

        public override CommonFileNode CreateCodeFileNode(ProjectElement item)
        {
            return new PowerShellFileNode(this, item);
        }

        protected override ConfigProvider CreateConfigProvider()
        {
            return new PowerShellConfigProvider(_package, this);
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new PowerShellProjectNodeProperties(this);
        }
    }
}
