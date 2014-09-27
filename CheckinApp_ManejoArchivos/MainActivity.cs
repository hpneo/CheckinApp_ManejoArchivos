using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.IO;
using System.Collections;

namespace CheckinApp_ManejoArchivos
{
	[Activity (Label = "CheckinApp", MainLauncher = true, Icon = "@drawable/icon", Theme="@android:style/Theme.Holo.Light")]
	public class MainActivity : Activity
	{
		private ListView listViewMovies;
		private EditText editTextNewMovie;
		private Button buttonAddMovie;
		private ArrayAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			var directoryPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			var appDirectoryPath = Path.Combine (directoryPath, "CheckinApp");
			var filePath = Path.Combine (appDirectoryPath, "movies.txt");

			if (!Directory.Exists (appDirectoryPath)) {
				Directory.CreateDirectory (appDirectoryPath);
			}

			if (!File.Exists (filePath)) {
				File.CreateText (filePath).Close ();
			}

			editTextNewMovie = FindViewById<EditText> (Resource.Id.editTextNewMovie);
			buttonAddMovie = FindViewById<Button> (Resource.Id.buttonAddMovie);

			listViewMovies = FindViewById<ListView> (Resource.Id.listViewMovies);
			adapter = new ArrayAdapter (this, Resource.Layout.MovieItem, new string[] { });

			listViewMovies.Adapter = adapter;

			buttonAddMovie.Click += (object sender, EventArgs e) => {
				string newMovie = editTextNewMovie.Text.Trim();
				if (newMovie != "") {
					if (adapter.Count == 0) {
						File.AppendAllText(filePath, newMovie);
					}
					else {
						File.AppendAllText(filePath, "\n" + newMovie);
					}
					adapter.Add(newMovie);
				}
			};

			string[] lines = File.ReadAllLines (filePath, new System.Text.UTF8Encoding());

			for (int i = 0; i < lines.Length; i++) {
				adapter.Add (lines [i]);
			}l
		}
	}
}


