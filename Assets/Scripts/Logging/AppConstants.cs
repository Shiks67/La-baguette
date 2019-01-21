using System;

public static class AppConstants
{

    #region General Constants

    public static string DefaultEyeTrackingFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Arthur_GazeData";
    #endregion

    // not used
    #region OSCSystem constants
    public const string CalibrationCubeTag = "CCube";
    public const float CubeScaleRate = 0.75f;
    public const float CubeDistance = 2.0f;
    public const float CubeDepth = 0.1f;
    public const float TimeOutBeforeSizeUp = 1000f;
    public const int GridHeight = 3;
    public const int GridWidth = 3;
    #endregion

    #region LoggerBehavior Constants

    public const string CsvFirstRow = "User ID;Date - Time;Session Time;Scene Name;Scene Timer;framerate;circle_pos_x;circle_pos_y; " +
     "pupilData_gaze_x;pupilData_gaze_y;gaze_on_grid_x;gaze_on_grid_y;Left_eye_conf;Right_eye_conf;gaze_confidence;" +
     "circle_radius;Time_To_First_Fix;";

    #endregion
}
