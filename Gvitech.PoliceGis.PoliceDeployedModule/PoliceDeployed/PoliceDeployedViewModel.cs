using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Microsoft.Office.Interop.Word;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.PoliceDeployedModule.PoliceDeployed
{
    // Token: 0x02000011 RID: 17
    public class PoliceDeployedViewModel : CheckedToolItemModel
    {
        // Token: 0x0600005A RID: 90 RVA: 0x00005194 File Offset: 0x00003394
        private void BuildDoc(string filePath, ExportProgressView progressView, DeployPlan plan)
        {
            plan.IsSelected = true;
            plan.SetDepolyCamera();
            plan.Snapshot = AppDomain.CurrentDomain.BaseDirectory + "temp\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
            GviMap.AxMapControl.RcPictureExportEnd += new _IRenderControlEvents_RcPictureExportEndEventHandler(RenderControl_RcPictureExportEnd);
            GviMap.AxMapControl.RcPictureExporting += new _IRenderControlEvents_RcPictureExportingEventHandler(RenderControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportBegin += new _IRenderControlEvents_RcPictureExportBeginEventHandler(RenderControl_RcPictureExportBegin);
            Console.WriteLine("开始出图");
            GviMap.MapControl.ExportManager.ExportImage(plan.Snapshot, 1024u, 768u, false);
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00005254 File Offset: 0x00003454
        private void RenderControl_RcPictureExportBegin(int NumberOfWidth, int NumberOfHeight)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
            this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartGetPic"), this.snapshotCount + 1);
            });
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00005274 File Offset: 0x00003474
        private void RenderControl_RcPictureExporting(int Index, float Percentage)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartGetPicProgress"), this.snapshotCount + 1, (int)(Percentage * 100f));
            });
        }

        // Token: 0x0600005D RID: 93 RVA: 0x000052B4 File Offset: 0x000034B4
        private void RenderControl_RcPictureExportEnd(double Time, bool IsAborted)
        {
            try
            {
                GviMap.AxMapControl.RcPictureExportEnd -= new _IRenderControlEvents_RcPictureExportEndEventHandler(RenderControl_RcPictureExportEnd);
                GviMap.AxMapControl.RcPictureExporting -= new _IRenderControlEvents_RcPictureExportingEventHandler(RenderControl_RcPictureExporting);
                this.ExportSnapshotEnd(this.filePath, this.progressView, this.tempPlan, Time);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        // Token: 0x0600005E RID: 94 RVA: 0x0000530C File Offset: 0x0000350C
        private void ExportSnapshotEnd(string docPath, ExportProgressView progressView, DeployPlan plan, double s)
        {
            int num = this.snapshotCount;
            this.snapshotCount = num + 1;
            bool flag = this.snapshotCount != this.DeployPlans.Count;
            if (flag)
            {
                this.tempPlan = this.DeployPlans[this.DeployPlans.IndexOf(plan) + 1];
                this.BuildDoc(docPath, progressView, this.tempPlan);
            }
            Console.WriteLine("RcPictureExportEnd:" + s);
            bool flag2 = this.snapshotCount == this.DeployPlans.Count;
            if (flag2)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(delegate
                {
                    progressView.ViewModel.ProgressValue = Helpers.ResourceHelper.FindKey("Beingoutput");
                });
                object obj = docPath;
                Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.ApplicationClass();
                object value = Missing.Value;
                Document document = application.Documents.Add(ref value, ref value, ref value, ref value);
                application.Selection.Font.Bold = 700;
                application.Selection.Font.Size = 16f;
                application.Selection.Range.ParagraphFormat.Alignment = (WdParagraphAlignment)1;
                application.Selection.Text = Helpers.ResourceHelper.FindKey("Policedetails");
                foreach (DeployPlan deployPlan in this.DeployPlans)
                {
                    string snapshot = deployPlan.Snapshot;
                    deployPlan.IsSelected = true;
                    deployPlan.SetDepolyCamera();
                    int num2 = deployPlan.Polices.Count + 1;
                    int num3 = 4;
                    Range range = application.Selection.Range;
                    object obj2 = value;
                    object obj3 = value;
                    range.MoveEnd(ref obj2, ref obj3);
                    object obj4 = 14;
                    object obj5 = 5;
                    Selection selection = application.Selection;
                    obj3 = value;
                    selection.MoveDown(ref obj5, ref obj4, ref obj3);
                    application.Selection.TypeParagraph();
                    Table table = document.Tables.Add(application.Selection.Range, num2, num3, ref value, ref value);
                    table.Cell(1, 1).Range.Text = "Id(警员,警车)";
                    table.Cell(1, 2).Range.Text = "名称(警员,车牌号)";
                    table.Cell(1, 3).Range.Text = "目标距离(米)";
                    table.Cell(1, 4).Range.Text = "估计花费时间";
                    table.Rows.HeightRule = (WdRowHeightRule)1;
                    table.Rows.Height = application.CentimetersToPoints(float.Parse("0.8"));
                    table.Range.Font.Size = 10.5f;
                    table.Range.Font.Bold = 0;
                    table.Range.ParagraphFormat.Alignment = (WdParagraphAlignment)1;
                    table.Range.Cells.VerticalAlignment = (WdCellVerticalAlignment)1;
                    table.Borders.OutsideLineStyle = (WdLineStyle)1;
                    table.Borders.InsideLineStyle = (WdLineStyle)1;
                    table.Rows[1].Range.Font.Bold = 1;
                    table.Rows[1].Range.Font.Size = 12f;
                    table.Cell(1, 1).Range.Font.Size = 10.5f;
                    for (int i = 0; i < deployPlan.Polices.Count; i = num + 1)
                    {
                        table.Cell(i + 2, 1).Range.Text = deployPlan.Polices[i].ID;
                        table.Cell(i + 2, 2).Range.Text = deployPlan.Polices[i].Name;
                        table.Cell(i + 2, 3).Range.Text = deployPlan.Polices[i].Distance.ToString();
                        table.Cell(i + 2, 4).Range.Text = deployPlan.Polices[i].UseTime;
                        num = i;
                    }
                    object range2 = document.Paragraphs.Last.Range;
                    object obj6 = false;
                    object obj7 = true;
                    document.InlineShapes.AddPicture(snapshot, ref obj6, ref obj7, ref range2);
                    object obj8 = 6;
                    application.Selection.ParagraphFormat.Alignment = (WdParagraphAlignment)1;
                    document.Content.InsertAfter("\n");
                    application.Selection.EndKey(ref obj8, ref value);
                    application.Selection.ParagraphFormat.Alignment = (WdParagraphAlignment)1;
                    application.Selection.Font.Size = 10f;
                    application.Selection.TypeText(Path.GetFileNameWithoutExtension(snapshot) + "\n");
                }
                object obj9 = 0;
                document.SaveAs(ref obj, ref obj9, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
                document.Close(ref value, ref value, ref value);
                application.Quit(ref value, ref value, ref value);
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
                this.snapshotCount = 0;
                docPath = string.Empty;
                System.Windows.Application.Current.Dispatcher.Invoke(delegate
                {
                    progressView.ViewModel.ProgressValue = string.Empty;
                });
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Deploymentcomplete"));
            }
        }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600005F RID: 95 RVA: 0x00005894 File Offset: 0x00003A94
        // (set) Token: 0x06000060 RID: 96 RVA: 0x000058AC File Offset: 0x00003AAC
        [XmlIgnore]
        public ObservableCollection<DeployPlan> DeployPlans
        {
            get
            {
                return this.deployPlan;
            }
            set
            {
                this.deployPlan = value;
            }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000061 RID: 97 RVA: 0x000058B6 File Offset: 0x00003AB6
        // (set) Token: 0x06000062 RID: 98 RVA: 0x000058BE File Offset: 0x00003ABE
        [XmlIgnore]
        public ICommand BuildDeployDocCmd { get; set; }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000063 RID: 99 RVA: 0x000058C7 File Offset: 0x00003AC7
        // (set) Token: 0x06000064 RID: 100 RVA: 0x000058CF File Offset: 0x00003ACF
        [XmlIgnore]
        public ICommand CreateDeployCmd { get; set; }

        // Token: 0x06000065 RID: 101 RVA: 0x000058D8 File Offset: 0x00003AD8
        public override void Reset()
        {
            base.Reset();
            bool isChecked = base.IsChecked;
            if (isChecked)
            {
                base.IsChecked = false;
            }
            IEnumerableExtension.ForEach<DeployPlan>(this.deployPlan, delegate (DeployPlan plan)
            {
                plan.DeleteLines();
            });
        }

        // Token: 0x06000066 RID: 102 RVA: 0x0000592C File Offset: 0x00003B2C
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            this.CreateDeployCmd = new RelayCommand(() =>
            {
                this.DeployPlans.Add(new DeployPlan(this.DeployPlans)
                {
                    Name = "目标点"
                });
            });
            this.BuildDeployDocCmd = new RelayCommand(() =>
            {
                bool flag2 = this.DeployPlans.Count == 0;
                if (flag2)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Pleasedeployfirst"));
                }
                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "word文档|*.doc";
                        bool flag3 = saveFileDialog.ShowDialog() == DialogResult.OK;
                        if (flag3)
                        {
                            this.filePath = saveFileDialog.FileName;
                        }
                    }
                    bool flag4 = string.IsNullOrEmpty(this.filePath);
                    if (!flag4)
                    {
                        ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(delegate
                        {
                            try
                            {
                                this.tempPlan = this.DeployPlans.FirstOrDefault<DeployPlan>();
                                this.BuildDoc(this.filePath, this.progressView, this.tempPlan);
                            }
                            catch (Exception ex)
                            {
                                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
                                SystemLog.Log(ex);
                                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Enerationfailed"));
                            }
                        }), DispatcherPriority.Background, new object[0]);
                    }
                }
            });
            SimpleCommandEx simpleCommandEx = (SimpleCommandEx)ServiceManager.GetService<IShellService>(null).HomeCmd;
            bool flag = simpleCommandEx != null;
            if (flag)
            {
                simpleCommandEx.CommandCompleted += delegate (object s, EventArgs e)
                {
                    object content = ServiceManager.GetService<IShellService>(null).RightView.Content;
                    bool flag2 = content != null && content is PoliceDeployedView;
                    if (flag2)
                    {
                        PoliceDeployedViewModel policeDeployedViewModel = (content as PoliceDeployedView).DataContext as PoliceDeployedViewModel;
                        IEnumerableExtension.ForEach<DeployPlan>(policeDeployedViewModel.DeployPlans, delegate (DeployPlan p)
                        {
                            p.IsSelected = false;
                        });
                    }
                };
            }
        }

        // Token: 0x06000067 RID: 103 RVA: 0x000059BC File Offset: 0x00003BBC
        public override void OnChecked()
        {
            base.OnChecked();
            bool flag = this.policeDeployedView == null;
            if (flag)
            {
                this.policeDeployedView = new PoliceDeployedView();
                this.policeDeployedView.Owner = System.Windows.Application.Current.MainWindow;
                this.policeDeployedView.DataContext = this;
                this.policeDeployedView.Left = SystemParameters.PrimaryScreenWidth - this.policeDeployedView.Width - 100.0;
                this.policeDeployedView.Top = 100.0;
            }
            this.policeDeployedView.Show();
        }

        // Token: 0x06000068 RID: 104 RVA: 0x00005A58 File Offset: 0x00003C58
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            bool flag = this.policeDeployedView != null;
            if (flag)
            {
                this.policeDeployedView.Hide();
            }
            IEnumerableExtension.ForEach<DeployPlan>(this.deployPlan, delegate (DeployPlan plan)
            {
                plan.RestoreEnv();
            });
        }

        // Token: 0x0400003B RID: 59
        private readonly ExportProgressView progressView = new ExportProgressView();

        // Token: 0x0400003C RID: 60
        private string filePath = string.Empty;

        // Token: 0x0400003D RID: 61
        private int snapshotCount;

        // Token: 0x0400003E RID: 62
        private DeployPlan tempPlan;

        // Token: 0x0400003F RID: 63
        private ObservableCollection<DeployPlan> deployPlan = new ObservableCollection<DeployPlan>();

        // Token: 0x04000042 RID: 66
        private System.Windows.Window policeDeployedView;
    }
}
