using BizHawk.Emulation.Cores.Nintendo.NES;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.EmuHawk
{
	public partial class NESSoundConfig : ToolFormBase
	{
		[RequiredService]
		private NES NES { get; set; }

		private NES.NESSettings _oldSettings;
		private NES.NESSettings _settings;

		public override void Restart()
			=> NESSoundConfig_Load(null, EventArgs.Empty);

		protected override string WindowTitleStatic => "NES Sound Channels";

		public NESSoundConfig()
		{
			InitializeComponent();

			// get baseline maxes from a default config object
			var d = new NES.NESSettings();
			trackBar1.Minimum = d.APU_vol;
			chkLinearMixer.Checked = d.LinearMixer;
			chkNotResetPhase.Checked = d.NotResetPhase;
			chkSwapDutyCycles.Checked = d.SwapDutyCycles;
		}

		private void NESSoundConfig_Load(object sender, EventArgs e)
		{
			_oldSettings = NES.GetSettings();
			_settings = _oldSettings.Clone();

			trackBar1.Value = _settings.APU_vol;
			chkLinearMixer.Checked = _settings.LinearMixer;
			chkNotResetPhase.Checked = _settings.NotResetPhase;
			chkSwapDutyCycles.Checked = _settings.SwapDutyCycles;
		}

		private void Ok_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			// restore previous value
			NES.PutSettings(_oldSettings);
			Close();
		}

		private void TrackBar1_ValueChanged(object sender, EventArgs e)
		{
			label6.Text = trackBar1.Value.ToString();
			_settings.APU_vol = trackBar1.Value;
			NES.PutSettings(_settings);
		}

		private void chkNonlinearSquareMixer_CheckedChanged(object sender, EventArgs e)
		{
			_settings.LinearMixer = chkLinearMixer.Checked;
			NES.PutSettings(_settings);
		}

		private void chkResetSquarePhase_CheckedChanged(object sender, EventArgs e)
		{
			_settings.NotResetPhase = chkNotResetPhase.Checked;
			NES.PutSettings(_settings);
		}

		private void chkSwapDutyCycles_CheckedChanged(object sender, EventArgs e)
		{
			_settings.SwapDutyCycles = chkSwapDutyCycles.Checked;
			NES.PutSettings(_settings);
		}
	}
}
