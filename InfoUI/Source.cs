using RUIModule.RUIElements;

namespace IntegratedInfo.InfoUI
{
    public partial class InfoPanel
    {
        private UIBottom sourcePanel;
        private UIContainerPanel sourceView;
        private void RegisterSourcePanel(UICornerPanel bg)
        {
            sourcePanel = new();
            sourcePanel.SetPos(0, 62);
            sourcePanel.SetSize(0, -62, 1, 1);
            bg.Register(sourcePanel);

            UICornerPanel sourceBg = new();
            sourceBg.SetSize(0, 0, 1, 1);
            sourcePanel.Register(sourceBg);

            sourceView = new(10);
            sourceView.SetSize(-20, 0, 1, 1);
            sourceView.VerticalEdge();
            sourceView.autoPos = [5, 5];
            sourceBg.Register(sourceView);

            VerticalScrollbar sv = new(39);
            sourceView.SetVerticalScrollbar(sv);
            sourceBg.Register(sv);
        }
    }
}
