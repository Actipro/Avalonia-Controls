namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted {

	/// <summary>
	/// Defines the information for a step in a Getting Started series.
	/// </summary>
	public class GettingStartedItemInfo {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="GettingStartedItemInfo"/> class.
		/// </summary>
		/// <param name="stepNumber">The number of the step.</param>
		/// <param name="path">The path fo the class which defines the sample.</param>
		/// <param name="summary">The summary of the step.</param>
		public GettingStartedItemInfo(int stepNumber, string path, string summary) {
			this.StepNumber = stepNumber;
			this.Path = path;
			this.Summary = summary;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the path of the class which defines the sample.
		/// </summary>
		/// <value>A string value.</value>
		public string Path { get; }

		/// <summary>
		/// Gets the number of the step.
		/// </summary>
		/// <value>An integer value.</value>
		public int StepNumber { get; }

		/// <summary>
		/// Gets the summary of the step.
		/// </summary>
		/// <value>A string value.</value>
		public string Summary { get; }

	}

}
