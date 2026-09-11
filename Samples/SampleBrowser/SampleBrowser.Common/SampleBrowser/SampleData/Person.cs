using ActiproSoftware.UI.Avalonia.Media;
using Avalonia.Platform;

namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Represents a person.
/// </summary>
public class Person {

	private DrawingImage? _photo;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="id">The ID.</param>
	/// <param name="lastName">The last name.</param>
	/// <param name="firstName">The first name.</param>
	/// <param name="emailAddress">The e-mail address.</param>
	/// <param name="position">The position.</param>
	/// <param name="hireDate">The hire date.</param>
	/// <param name="photoUri">The photo URI.</param>
	public Person(int id, string lastName, string firstName, string emailAddress, string position, DateTime hireDate, Uri photoUri) {
		Id = id;
		LastName = lastName;
		FirstName = firstName;
		EmailAddress = emailAddress;
		Position = position;
		HireDate = hireDate;
		PhotoUri = photoUri;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

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
	public IImage Photo {
		get {
			if (_photo is null) {
				// Bitmap is not an AvaloniaObject and doesn't support attached properties, so wrap it in a DrawingImage that does
				_photo = new DrawingImage {
					Drawing = new ImageDrawing {
						ImageSource = new Bitmap(AssetLoader.Open(PhotoUri)),
						Rect = new Rect(0, 0, 192, 192)
					}
				};

				// Prevent the photo from being adapted for dark themes
				ImageProvider.SetCanAdapt(_photo, false);
			}

			return _photo;
		}
	}

	/// <summary>
	/// The photo URI.
	/// </summary>
	public Uri PhotoUri { get; }

	/// <summary>
	/// The position.
	/// </summary>
	public string Position { get; }

}
