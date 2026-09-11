using ActiproSoftware.UI.Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.AnimatedTextPresenterIntro;

public partial class MainControl : UserControl {

	private string _bidirectionalPairName = "SignedCurrency";
	private int _counterValue = 1999;
	private int _dashboardSnapshotIndex;
	private readonly DispatcherTimer _dashboardTimer = new() { Interval = TimeSpan.FromSeconds(2.0) };
	private bool _isAttachedToVisualTree;
	private bool _isBidirectionalPairUsingSecondText;
	private bool _isInitialized;

	private static readonly (
		string RequestsText,
		string RequestsTrendText,
		string RequestsTrendSemanticClass,
		string BuildText,
		string? BuildSemanticClass,
		string IssuesText,
		string IssuesTrendText,
		string IssuesTrendSemanticClass
	)[] _dashboardSnapshots = [
		("1,284", "+12%", SuccessClass, "Passed", SuccessClass, "42", "-8", SuccessClass),
		("1,305", "+7%", SuccessClass, "Running", null, "38", "-4", SuccessClass),
		("984", "-11%", DangerClass, "Failed", DangerClass, "41", "+3", DangerClass),
		("1,472", "+13%", SuccessClass, "Passed", SuccessClass, "35", "-6", SuccessClass),
	];
	private const string AccentClass = "accent";
	private const string DangerClass = "danger";
	private const string SuccessClass = "success";
	private const string WarningClass = "warning";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		InitializeComponent();
		
		_isInitialized = true;
		
		_dashboardTimer.Tick += OnDashboardTimerTick;
		rightToLeftTextPresenter.PropertyChanged += OnRightToLeftTextPresenterPropertyChanged;

		UpdateBidirectionalTextPair();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void ApplyBidirectionalTextPair(string leftToRightFirstText, string leftToRightSecondText, string rightToLeftFirstText, string rightToLeftSecondText) {
		leftToRightTextPresenter.Text = _isBidirectionalPairUsingSecondText ? leftToRightSecondText : leftToRightFirstText;
		rightToLeftTextPresenter.Text = _isBidirectionalPairUsingSecondText ? rightToLeftSecondText : rightToLeftFirstText;
	}

	private void ApplyDashboardSnapshot(int index) {
		var snapshot = _dashboardSnapshots[index];

		requestsTextPresenter.Text = snapshot.RequestsText;
		requestsTrendTextPresenter.Text = snapshot.RequestsTrendText;
		SetSemanticClass(requestsTrendTextPresenter, snapshot.RequestsTrendSemanticClass);
		buildTextPresenter.Text = snapshot.BuildText;
		SetSemanticClass(buildTextPresenter, snapshot.BuildSemanticClass);
		issuesTextPresenter.Text = snapshot.IssuesText;
		issuesTrendTextPresenter.Text = snapshot.IssuesTrendText;
		SetSemanticClass(issuesTrendTextPresenter, snapshot.IssuesTrendSemanticClass);
	}

	private static string FormatSignedCurrency(decimal value, CultureInfo culture) {
		var text = value.ToString("C2", culture);
		return (value > 0.0m) ? culture.NumberFormat.PositiveSign + text : text;
	}

	private void OnBidirectionalScenarioSelectionChanged(object? sender, SelectionChangedEventArgs e) {
		if (!_isInitialized || (sender is not ComboBox { SelectedValue: string pairName }))
			return;

		_bidirectionalPairName = pairName;
		_isBidirectionalPairUsingSecondText = false;
		ResetBidirectionalTextPair();
	}

	private void OnBidirectionalToggleButtonClick(object? sender, RoutedEventArgs e) {
		_isBidirectionalPairUsingSecondText = !_isBidirectionalPairUsingSecondText;
		UpdateBidirectionalTextPair();
	}

	private void OnCounterAdjustmentButtonClick(object? sender, RoutedEventArgs e) {
		if (
			(sender is not RepeatButton { Tag: string adjustmentText })
			|| !int.TryParse(adjustmentText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var adjustment)
		)
			return;

		_counterValue += adjustment;
		counterTextPresenter.Text = $"Count: {_counterValue}";
	}

	private void OnDashboardAutoUpdateIsCheckedChanged(object? sender, RoutedEventArgs e)
		=> UpdateDashboardTimerActivation();

	private void OnDashboardTimerTick(object? sender, EventArgs e) {
		_dashboardSnapshotIndex = (_dashboardSnapshotIndex + 1) % _dashboardSnapshots.Length;
		ApplyDashboardSnapshot(_dashboardSnapshotIndex);
	}

	private void OnQuoteUpdateButtonClick(object? sender, RoutedEventArgs e) {
		ToggleTextPair(quotePriceTextPresenter, "$181.42|$182.07");
		ToggleTextPair(quoteStatusTextPresenter, "Delayed|Live");
	}

	private void OnRightToLeftTextPresenterPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
		if (e.Property == AnimatedTextPresenter.CultureProperty)
			Dispatcher.UIThread.Post(UpdateBidirectionalTextPair);
	}

	private void OnSemanticNumericScenarioSelectionChanged(object? sender, SelectionChangedEventArgs e) {
		if (
			_isInitialized
			&& (sender is ComboBox { SelectedValue: string pairText })
			&& TryGetTextPair(pairText, out var firstText, out _)
		) {
			semanticNumericTextPresenter.Text = firstText;
		}
	}

	private void OnTextPairButtonClick(object? sender, RoutedEventArgs e) {
		if (sender is Button { DataContext: AnimatedTextPresenter textPresenter, Tag: string pairText })
			ToggleTextPair(textPresenter, pairText);
	}

	private void ResetBidirectionalTextPair() {
		var isAnimationEnabled = rightToLeftTextPresenter.IsAnimationEnabled;
		rightToLeftTextPresenter.IsAnimationEnabled = false;
		UpdateBidirectionalTextPair();
		rightToLeftTextPresenter.IsAnimationEnabled = isAnimationEnabled;
	}

	private static void SetSemanticClass(AnimatedTextPresenter textPresenter, string? semanticClass) {
		if ((semanticClass is not null) && textPresenter.Classes.Contains(semanticClass))
			return;

		textPresenter.Classes.Remove(AccentClass);
		textPresenter.Classes.Remove(DangerClass);
		textPresenter.Classes.Remove(SuccessClass);
		textPresenter.Classes.Remove(WarningClass);
		if (semanticClass is not null)
			textPresenter.Classes.Add(semanticClass);
	}

	private static void ToggleTextPair(AnimatedTextPresenter textPresenter, string pairText) {
		if (!TryGetTextPair(pairText, out var firstText, out var secondText))
			return;

		textPresenter.Text = StringComparer.Ordinal.Equals(textPresenter.Text, firstText) ? secondText : firstText;
	}

	private static bool TryGetTextPair(string pairText, out string firstText, out string secondText) {
		var separatorIndex = pairText.IndexOf('|', StringComparison.Ordinal);
		if (separatorIndex < 0) {
			firstText = pairText;
			secondText = pairText;
			return false;
		}

		firstText = pairText[..separatorIndex];
		secondText = pairText[(separatorIndex + 1)..];
		return true;
	}

	private void UpdateBidirectionalTextPair() {
		var culture = rightToLeftTextPresenter.Culture ?? CultureInfo.CurrentCulture;
		switch (_bidirectionalPairName) {
			case "MultipleNumbers":
				ApplyBidirectionalTextPair(
					$"Step {2.ToString(culture)} of {120.ToString(culture)}",
					$"Step {10.ToString(culture)} of {99.ToString(culture)}",
					$"\u0627\u0644\u062e\u0637\u0648\u0629 {2.ToString(culture)} \u0645\u0646 {120.ToString(culture)}",
					$"\u0627\u0644\u062e\u0637\u0648\u0629 {10.ToString(culture)} \u0645\u0646 {99.ToString(culture)}"
				);
				break;
			case "SignedCurrency":
				ApplyBidirectionalTextPair(
					$"Balance: {FormatSignedCurrency(1245.50m, culture)}",
					$"Balance: {FormatSignedCurrency(-989.00m, culture)}",
					$"\u0627\u0644\u0631\u0635\u064a\u062f: {FormatSignedCurrency(1245.50m, culture)}",
					$"\u0627\u0644\u0631\u0635\u064a\u062f: {FormatSignedCurrency(-989.00m, culture)}"
				);
				break;
			case "Percentage":
				ApplyBidirectionalTextPair(
					$"Total: {0.09m.ToString("0%", culture)}",
					$"Total: {1.0m.ToString("0%", culture)}",
					$"\u0627\u0644\u0625\u062c\u0645\u0627\u0644\u064a: {0.09m.ToString("0%", culture)}",
					$"\u0627\u0644\u0625\u062c\u0645\u0627\u0644\u064a: {1.0m.ToString("0%", culture)}"
				);
				break;
		}
	}

	private void UpdateDashboardTimerActivation() {
		if (
			_isInitialized
			&& _isAttachedToVisualTree
			&& IsEffectivelyVisible
			&& (dashboardAutoUpdateToggleSwitch.IsChecked == true)
		)
			_dashboardTimer.Start();
		else
			_dashboardTimer.Stop();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
		base.OnAttachedToVisualTree(e);

		_isAttachedToVisualTree = true;
		UpdateDashboardTimerActivation();
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
		base.OnDetachedFromVisualTree(e);

		_isAttachedToVisualTree = false;
		UpdateDashboardTimerActivation();
	}

	protected override void OnLoaded(RoutedEventArgs e) {
		base.OnLoaded(e);

		UpdateDashboardTimerActivation();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) {
		base.OnPropertyChanged(change);

		if (_isInitialized && (change.Property.Name == nameof(IsEffectivelyVisible)))
			UpdateDashboardTimerActivation();
	}

}
