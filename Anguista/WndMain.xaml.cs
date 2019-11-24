// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System;
using System.IO;        // for Path
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Remoting;
using Sysphonic.Common;
using Anguista.Lib;
using System.Windows.Forms;

namespace Anguista.Main
{
    /// <summary>Settings Window class.</summary>
    public partial class WndMain : Window
    {
        /// <summary>URI of the Channel Icon.</summary>
        protected const string CHANNEL_ICON_URI = @"pack://application:,,,/Resources/ball_blue.png";

        /// <summary>URI of the Item Icon.</summary>
        protected const string ITEM_ICON_URI = @"pack://application:,,,/Resources/file.png";

        /// <summary>Node type of Trash Box : Target.</summary>
        protected const string TRASH_NODE_TARGET = @"target";

        /// <summary>Node type of Trash Box : Channel.</summary>
        protected const string TRASH_NODE_CHANNEL = @"channel";

        /// <summary>Node type of Trash Box : Item.</summary>
        protected const string TRASH_NODE_ITEM = @"item";

        /// <summary>Configuration manager.</summary>
        protected ConfigManager _configManager = null;

        /// <summary>Configuration for AnguistaConf.</summary>
        private ConfConfig _confConfig = null;

        /// <summary>RSS targets (non-Zeptair Dist.).</summary>
        protected ArrayList _rssTargets = null;

        /// <summary>Zeptair Distribution targets.</summary>
        protected ArrayList _zeptDistTargets = null;

        /// <summary>Deleted RSS Informations.</summary>
        protected ArrayList _deletedRssInfos = new ArrayList();

        /// <summary>Modified flag of the Target List.</summary>
        protected bool _targetsModified = false;

        /// <summary>Trash Boxes.</summary>
        protected ArrayList _trashBoxes = null;

        /// <summary>Modified flag of the General Configuration.</summary>
        protected bool _configModified = false;

        /// <summary>Mutex to check if the same process exists.</summary>
        private static System.Threading.Mutex _mutex;

        /// <summary>Feed icon.</summary>
        BitmapImage _feedIcon = null;


        /// <summary>Zeptair icon.</summary>
        BitmapImage _zeptairIcon = null;

        /// <summary>Feed channel icon.</summary>
        BitmapImage _channelIcon = null;

        /// <summary>Feed item icon.</summary>
        BitmapImage _itemIcon = null;

        public delegate void UpdateGuiDelegate();


        /// <summary>Constructor.</summary>
        public WndMain()
        {
            InitializeComponent();

            _CheckProcessDuplicated();

            _configManager = ConfigManager.Load();
            _confConfig = ConfConfig.Load();

            Width = _confConfig.SettingsWidth;
            Height = _confConfig.SettingsHeight;
            
            txbConfigMaxPanels.Text = _configManager.MaxItemsOnPanel.ToString();

            _channelIcon = WpfUtil.LoadImage(CHANNEL_ICON_URI);
            _itemIcon = WpfUtil.LoadImage(ITEM_ICON_URI);

        }

        private void _AddTrashBox()
        {
         }

        /// <summary>Loaded event handler.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void wndMain_Loaded(object sender, EventArgs e)
        {
            /*
            if (_targets.Count <= 0)
            {
                tbiTargets.IsSelected = true;
                btnTargetAdd.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, btnTargetAdd));
            }
            */
        }

        /// <summary>Checks if the same process exists.</summary>
        private void _CheckProcessDuplicated()
        {
            _mutex = new System.Threading.Mutex(false, Anguista.Lib.Def.PROC_ID_THETISCORE_CONF);

            if (_mutex.WaitOne(0, false) == false)
            {
                Process prevProcess = CommonUtil.GetPreviousProcess();
                if (prevProcess != null)
                {
                    CommonUtil.WakeupWindow(prevProcess.MainWindowHandle);
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>Forces to close.</summary>
        public void ForceClose()
        {
            this.Close();
        }

        /// <summary>TaskProcClosing event handler of IpcConfService.</summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        public void ipcConfService_TaskProcClosing(object sender, Anguista.Lib.TaskProcClosingEventArgs e)
        {
            this.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new UpdateGuiDelegate(this.ForceClose)
                  );
        }

        /// <summary>RSS Target Informations.</summary>
        public ArrayList RssTargetInfos
        {
            get { return _rssTargets; }
        }

        /// <summary>Zeptair Distribution Target Informations.</summary>
        public ArrayList ZeptDistTargetInfos
        {
            get { return _zeptDistTargets; }
        }

        /// <summary>Closing event handler.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void wndMain_Closing(object sender, EventArgs e)
        {
            _confConfig.SettingsWidth = Width;
            _confConfig.SettingsHeight = Height;
            _confConfig.Save();
        }

        /////////////////////////////////////////////////////////

        /// <summary>Click event handler of the Target Add button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnTargetAdd_Click(object sender, RoutedEventArgs e)
        {
            WndTarget wndTarget = new WndTarget();
            wndTarget.Owner = this;
            wndTarget.ShowDialog();
        }

        /// <summary>Click event handler of the Target Edit button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnTargetEdit_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>Click event handler of the Target Delete button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnTargetDelete_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>Updates or adds the specified item in the Target List.</summary>
        /// <param name="info">New RSS Information.</param>
        public void UpdateTargetList()
        {
        }

        /// <summary>Updates Target Informations.</summary>
        public void UpdateTargetInfos()
        {
        }

        /////////////////////////////////////////////////////////

        /// <summary>Click event handler of the Zeptair Dist. Add button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnZeptDistAdd_Click(object sender, RoutedEventArgs e)
        {
            WndTarget wndTarget = new WndTarget(true);
            wndTarget.Owner = this;
            wndTarget.ShowDialog();
        }

#if ZEPTAIR
        /// <summary>Click event handler of the Zeptair Dist. Edit button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnZeptDistEdit_Click(object sender, RoutedEventArgs e)
        {
            int selIdx = lstZeptDists.SelectedIndex;
            if (selIdx < 0)
                return;

            WndTarget wndTarget = new WndTarget((RssTargetInfo)_zeptDistTargets[selIdx]);
            wndTarget.Owner = this;
            wndTarget.ShowDialog();
        }

        /// <summary>Click event handler of the Zeptair Dist. Delete button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnZeptDistDelete_Click(object sender, RoutedEventArgs e)
        {
            int selIdx = lstZeptDists.SelectedIndex;
            if (selIdx < 0)
                return;

            RssTargetInfo info = (RssTargetInfo)_zeptDistTargets[selIdx];

            System.Windows.Forms.DialogResult ret = 
                        System.Windows.Forms.MessageBox.Show(
                                "\"" + info.Title + "\"" + Properties.Resources.CONFIRM_DELETE,
                                this.Title,
                                System.Windows.Forms.MessageBoxButtons.OKCancel,
                                System.Windows.Forms.MessageBoxIcon.Question
                            );
            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                if (info.CurPath != null)
                    _deletedRssInfos.Add(info);
                
                _zeptDistTargets.RemoveAt(selIdx);
                for (int i = selIdx; i < _zeptDistTargets.Count; i++)
                    ((RssTargetInfo)_zeptDistTargets[i]).Idx = i;
                
                lstZeptDists.Items.RemoveAt(selIdx);
                _targetsModified = true;

                _UpdateZeptDistAddButton();
            }
        }
#endif
        /////////////////////////////////////////////////////////

        /// <summary>Click event handler of the OK button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            btnApply_Click(sender, e);

            this.Close();
        }

        /// <summary>Click event handler of the Cancel button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>Click event handler of the Apply button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (_targetsModified)
            {
                UpdateTargetInfos();
                _targetsModified = false;
            }
            if (_configModified)
            {
                _configManager.MaxItemsOnPanel = Int32.Parse(txbConfigMaxPanels.Text);
                _configManager.Save();
                _configModified = false;
            }
        }

        /// <summary>Click event handler of the Trash Restore button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnTrashRestore_Click(object sender, RoutedEventArgs e)
        {
            TreeViewImgItem selItem = (TreeViewImgItem)trvTrash.SelectedItem;
            if (selItem == null)
                return;

            string selTitle = selItem.Text;
            if (selTitle.Length > 20)
                selTitle = selTitle.Substring(0, 20) + " ...";

            string msg = null;
            if (selItem.Type == TRASH_NODE_ITEM)
                msg = "\"" + selTitle + "\"" + Properties.Resources.CONFIRM_RESTORE_TRASH;
            else
                msg = Properties.Resources.CONFIRM_RESTORE_ALL_TRASH_1 + "\"" + selTitle + "\"" + Properties.Resources.CONFIRM_RESTORE_ALL_TRASH_2;

            System.Windows.Forms.DialogResult ret =
                        System.Windows.Forms.MessageBox.Show(
                                msg,
                                this.Title,
                                System.Windows.Forms.MessageBoxButtons.OKCancel,
                                System.Windows.Forms.MessageBoxIcon.Question
                            );
            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Anguista.Lib.IIpcTaskService ipcTaskService = Anguista.Lib.IpcServiceAgent.GetTaskService();
                    if (ipcTaskService == null)
                        return;

                    if (selItem.Type == TRASH_NODE_ITEM)
                    {
//                        InfoItem infoItem = (InfoItem)selItem.AdditionalInfo;
//                        ipcTaskService.RestoreTrash(infoItem.GeneratorId, new int[1] { infoItem.Id }); 

                        ((TreeViewImgItem)selItem.Parent).Items.Remove(selItem);
                    }
                    else if (selItem.Type == TRASH_NODE_CHANNEL)
                    {
                        if (selItem.Items.Count <= 0)
                            return;

                        string generatorId = null;
                        int[] itemIds = new int[selItem.Items.Count];

                        for (int i = 0; i < selItem.Items.Count; i++)
                        {
//                            InfoItem infoItem = (InfoItem)((TreeViewImgItem)selItem.Items[i]).AdditionalInfo;
//                           generatorId = infoItem.GeneratorId;
//                           itemIds[i] = infoItem.Id;
                        }
                        selItem.Items.Clear();

                        ipcTaskService.RestoreTrash(generatorId, itemIds);
                    }
                    else if (selItem.Type == TRASH_NODE_TARGET)
                    {
                        foreach (TreeViewImgItem channel in selItem.Items)
                        {
                            if (channel.Items.Count <= 0)
                                continue;

                            string generatorId = null;
                            int[] itemIds = new int[channel.Items.Count];

                            for (int i = 0; i < channel.Items.Count; i++)
                            {
                                //                                InfoItem infoItem = (InfoItem)((TreeViewImgItem)channel.Items[i]).AdditionalInfo;
                                //                                generatorId = infoItem.GeneratorId;
                                //                                itemIds[i] = infoItem.Id;
                            }
                            channel.Items.Clear();

                            ipcTaskService.RestoreTrash(generatorId, itemIds);
                        }
                    }
                }
                catch (System.Runtime.Remoting.RemotingException ex)
                {
                    Log.AddError(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        /// <summary>Click event handler of the Trash Delete button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnTrashDelete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewImgItem selItem = (TreeViewImgItem)trvTrash.SelectedItem;
            if (selItem == null)
                return;

            string selTitle = selItem.Text;
            if (selTitle.Length > 20)
                selTitle = selTitle.Substring(0, 20) + " ...";

            string msg = null;
            if (selItem.Type == TRASH_NODE_ITEM)
                msg = "\"" + selTitle + "\"" + Properties.Resources.CONFIRM_COMPLETELY_DELETE;
            else
                msg = Properties.Resources.CONFIRM_COMPLETELY_ALL_DELETE_1 + "\"" + selTitle + "\"" + Properties.Resources.CONFIRM_COMPLETELY_ALL_DELETE_2;
            
            System.Windows.Forms.DialogResult ret = 
                        System.Windows.Forms.MessageBox.Show(
                                msg,
                                this.Title,
                                System.Windows.Forms.MessageBoxButtons.OKCancel,
                                System.Windows.Forms.MessageBoxIcon.Question
                            );
            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Anguista.Lib.IIpcTaskService ipcTaskService = Anguista.Lib.IpcServiceAgent.GetTaskService();
                    if (ipcTaskService == null)
                        return;

                    if (selItem.Type == TRASH_NODE_ITEM)
                    {
//                        InfoItem infoItem = (InfoItem)selItem.AdditionalInfo;
//                        ipcTaskService.BurnUpTrash(infoItem.GeneratorId, new int[1] { infoItem.Id });

//                        ((TreeViewImgItem)selItem.Parent).Items.Remove(selItem);
                    }
                    else if (selItem.Type == TRASH_NODE_CHANNEL)
                    {
                        if (selItem.Items.Count <= 0)
                            return;

                        string generatorId = null;
                        int[] itemIds = new int[selItem.Items.Count];

                        for (int i = 0; i < selItem.Items.Count; i++)
                        {
                            //                            InfoItem infoItem = (InfoItem)((TreeViewImgItem)selItem.Items[i]).AdditionalInfo;
                            //                            generatorId = infoItem.GeneratorId;
                            //                            itemIds[i] = infoItem.Id;
                        }
                        selItem.Items.Clear();

                        ipcTaskService.BurnUpTrash(generatorId, itemIds);
                    }
                    else if (selItem.Type == TRASH_NODE_TARGET)
                    {
                        foreach (TreeViewImgItem channel in selItem.Items)
                        {
                            if (channel.Items.Count <= 0)
                                continue;

                            string generatorId = null;
                            int[] itemIds = new int[channel.Items.Count];

                            for (int i = 0; i < channel.Items.Count; i++)
                            {
//                                InfoItem infoItem = (InfoItem)((TreeViewImgItem)channel.Items[i]).AdditionalInfo;
//                                generatorId = infoItem.GeneratorId;
//                                itemIds[i] = infoItem.Id;
                            }
                            channel.Items.Clear();

                            ipcTaskService.BurnUpTrash(generatorId, itemIds);
                        }
                    }
                }
                catch (System.Runtime.Remoting.RemotingException ex)
                {
                    Log.AddError(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        /// <summary>Text Change event handler of the Config Max. Panels Text Box.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void txbConfigMaxPanels_TextChanged(object sender, TextChangedEventArgs e)
        {
            _configModified = true;
        }

        private void btnProjectPath_Click(object sender, RoutedEventArgs e)
        {
            var dlgFolderSelect = new DlgFolderSelect();
            dlgFolderSelect.Path = txbProjectPath.Text;
            if (dlgFolderSelect.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txbProjectPath.Text = dlgFolderSelect.Path;
                initProjectTree();
            }
        }

        public void initProjectTree()
        {
            String projectPath = txbProjectPath.Text;
            if (projectPath == "")
                return;

            _channelIcon = WpfUtil.LoadImage(CHANNEL_ICON_URI);
            _itemIcon = WpfUtil.LoadImage(ITEM_ICON_URI);

            Hashtable nodesHash = new Hashtable();

            TreeViewImgItem node = new TreeViewImgItem(TRASH_NODE_CHANNEL);
            node.Text = projectPath;
            node.SelectedImage = _channelIcon;
            node.IsExpanded = false;

            trvProject.Items.Add(node);
            nodesHash.Add("", node);

            try
            {
                DirectoryInfo dirList = new DirectoryInfo(projectPath);
                foreach (DirectoryInfo di in dirList.GetDirectories())
                {
                    TreeViewImgItem childNode = new TreeViewImgItem(TRASH_NODE_CHANNEL);
                    childNode.Text = di.Name;
                    childNode.SelectedImage = _itemIcon;
                    childNode.IsExpanded = false;

                    trvProject.Items.Add(childNode);
                }
            }
            catch (IOException ie)
            {
                // TODO
            }
        }
    }
}
