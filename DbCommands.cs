using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;


namespace DroidDb
{
	public class DbCommands
	{
		private DBHelper dbhelp;

		public DbCommands (Context context)
		{
			dbhelp = new DBHelper (context);
			dbhelp.OnCreate (dbhelp.WritableDatabase);
		}

		public IList<Score> GetAllScores()
		{
			Android.Database.ICursor golfCursor = dbhelp.ReadableDatabase.
				Query ("GolfScore", null, null, null, null, null, null);
			var scores = new List<Score> ();
			while (golfCursor.MoveToNext ()) 
			{
				Score scr = MapScores (golfCursor);
				scores.Add (scr);
			}

			return scores;

		} // GetAllScores

		public long AddScore(int ScoreNumber, DateTime ScoreDate, double rating, double slope)
		{

			var values = new ContentValues ();
			values.Put ("ScoreNumber", ScoreNumber);
			values.Put("ScoreDate", ScoreDate.ToString());
			values.Put ("Rating", rating);
			values.Put ("Slope", slope);

			return dbhelp.WritableDatabase.Insert ("GolfScore", null, values);


		} // AddScore

		public void DeleteScore(int ScoreID)
		{
			string[] vals = new string[1];
			vals [0] = ScoreID.ToString ();

			dbhelp.WritableDatabase.Delete ("GolfScore", "ScoreId=?", vals);

		} // DeleteScore

		private Score MapScores(Android.Database.ICursor cursor)
		{
			Score scr = new Score ();
			scr.ScoreID = cursor.GetInt (0);
			scr.ScoreDate = cursor.GetString (1);
			scr.ScoreNumber = cursor.GetInt (2);
			scr.Rating = cursor.GetDouble (3);
			scr.Slope = cursor.GetInt (4);

			return (scr);



		} // MapScores

	}
}

