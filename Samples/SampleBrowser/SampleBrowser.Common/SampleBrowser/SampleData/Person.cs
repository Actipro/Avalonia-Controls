using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Represents a person.
	/// </summary>
	public class Person {

		private IImage? _photo;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="id">The ID.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="emailAddress">The e-mail address.</param>
		/// <param name="position">The position.</param>
		/// <param name="hireDate">The hire date.</param>
		/// <param name="photoUri">The photo URI.</param>
		public Person(int id, string lastName, string firstName, string emailAddress, string position, DateTime hireDate, Uri photoUri) {
			this.Id = id;
			this.LastName = lastName;
			this.FirstName = firstName;
			this.EmailAddress = emailAddress;
			this.Position = position;
			this.HireDate = hireDate;
			this.PhotoUri = photoUri;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The e-mail address.
		/// </summary>
		public string EmailAddress { get; }
		
		/// <summary>
		/// The first name.
		/// </summary>
		public string FirstName { get; }
		
		/// <summary>
		/// The hire date.
		/// </summary>
		public DateTime HireDate { get; }

		/// <summary>
		/// The last name.
		/// </summary>
		public string LastName { get; }

		/// <summary>
		/// The full name.
		/// </summary>
		public string FullName
			=> $"{FirstName} {LastName}";

		/// <summary>
		/// The ID.
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// The photo loaded from the <see cref="PhotoUri"/>.
		/// </summary>
		public IImage Photo
			=> _photo ??= new Bitmap(AssetLoader.Open(PhotoUri));

		/// <summary>
		/// The photo URI.
		/// </summary>
		public Uri PhotoUri { get; }

		/// <summary>
		/// The position.
		/// </summary>
		public string Position { get; }

	}

}
