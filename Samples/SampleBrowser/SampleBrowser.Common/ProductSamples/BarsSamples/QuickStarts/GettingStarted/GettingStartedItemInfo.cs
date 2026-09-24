namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted;

/// <summary>
/// Defines the information for a step in a Getting Started series.
/// </summary>
/// <param name="StepNumber">The number of the step.</param>
/// <param name="Path">The path fo the class which defines the sample.</param>
/// <param name="Summary">The summary of the step.</param>
public record GettingStartedItemInfo(int StepNumber, string Path, string Summary);
