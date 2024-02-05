using System;
using System.Collections.Generic;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Provides access to people data.
	/// </summary>
	public static class People {

		private static List<Person>? _all;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets a collection of all people.
		/// </summary>
		/// <value>A collection of all people.</value>
		public static IList<Person> All {
			get {
				_all ??= new List<Person>() {
					new(3, "Barnes", "Harold", "harold.barnes@corpticaenterprises.com", "Vice President", new DateTime(1999, 1, 11), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Man03.jpg", UriKind.Absolute)),
					new(35, "Cazalla", "Miguel", "miguel.cazalla@corpticaenterprises.com", "Operator", new DateTime(2018, 12, 2), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Man01.jpg", UriKind.RelativeOrAbsolute)),
					new(21, "Dawson", "Barbara", "barb.dawson@corpticaenterprises.com", "Executive Assistant", new DateTime(2015, 8, 20), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman01.jpg", UriKind.RelativeOrAbsolute)),
					new(8, "Ellington", "Cliff", "cliff.ellington@corpticaenterprises.com", "Manager", new DateTime(2003, 6, 17), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Man02.jpg", UriKind.RelativeOrAbsolute)),
					new(31, "Fleming", "Michael", "michael.fleming@corpticaenterprises.com", "Operator", new DateTime(2017, 4, 4), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Man04.jpg", UriKind.RelativeOrAbsolute)),
					new(27, "Gi", "Jang", "jang.gi@corpticaenterprises.com", "Analyst", new DateTime(2016, 5, 25), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman03.jpg", UriKind.RelativeOrAbsolute)),
					new(4, "Harrison", "Ashley", "ashley.harrison@corpticaenterprises.com", "Sales Director", new DateTime(1999, 5, 9), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman06.jpg", UriKind.RelativeOrAbsolute)),
					new(26, "Lai", "Erik", "erik.lai@corpticaenterprises.com", "Operator", new DateTime(2016, 4, 24), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Man05.jpg", UriKind.RelativeOrAbsolute)),
					new(13, "Mitchell", "Tammy", "tammy.mitchell@corpticaenterprises.com", "Accountant", new DateTime(2011, 11, 20), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman02.jpg", UriKind.RelativeOrAbsolute)),
					new(17, "O'Connell", "Scarlette", "scarlette.oconnell@corpticaenterprises.com", "Assistant Manager", new DateTime(2013, 10, 5), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman05.jpg", UriKind.RelativeOrAbsolute)),
					new(1, "Taylor", "Tamara", "tamara.taylor@corpticaenterprises.com", "President", new DateTime(1996, 2, 19), new Uri("avares://SampleBrowser/Images/ProfilePhotos/Woman04.jpg", UriKind.RelativeOrAbsolute)),
				};

				return _all.AsReadOnly();
			}
		}

		/// <summary>
		/// Gets an arbitrary <see cref="Person"/> from the collection that does not change.
		/// </summary>
		public static Person Arbitrary
			=> All[3];

		/// <summary>
		/// Gets a random <see cref="Person"/>.
		/// </summary>
		public static Person Random {
			get {
				var index = new Random().Next(All.Count - 1);
				return All[index];
			}
		}

	}

}
