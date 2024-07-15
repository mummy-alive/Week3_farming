
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class MyConst {
    public static int PILE_SIZE = 64;
    public static int INVENTORY_SIZE = 24;
    public static int INVENTORY_ROWS = 3;
    public static int INVENTORY_COLUMNS = 8;
    public static int TOTAL_GAME_DAYS = 80;
    public static int MAX_HOURS_AWAKE = 18;
    public static int REAL_SECOND_PER_GAME_HOUR = 2;
    public static int BOAT_RETURN_DAYS = 1;

    private static List<double> _priceList = new List<double>
    {
        9.43333442e-01, 1.07665018e+00, 1.25182803e+00, 1.12488100e+00,
        1.36552579e+00, 1.37509899e+00, 1.80568107e+00, 2.00463503e+00,
        2.03753477e+00, 2.39160490e+00, 2.53776261e+00, 2.69742627e+00,
        2.85283244e+00, 2.95121860e+00, 3.28595329e+00, 3.71662840e+00,
        4.23213276e+00, 6.36152011e+00, 4.52228409e+00, 4.22422943e+00,
        5.30632266e+00, 4.01100754e+00, 5.78035287e+00, 7.09929225e+00,
        7.13300217e+00, 8.44226388e+00, 7.62629172e+00, 5.18234498e+00,
        6.55210045e+00, 8.42452530e+00, 9.08678025e+00, 9.49124037e+00,
        4.91849234e+00, 4.13841027e+00, 3.86371469e+00, 4.28264819e+00,
        5.97968525e+00, 5.50186791e+00, 4.70388780e+00, 6.06285226e+00,
        6.17389889e+00, 4.37017454e+00, 5.91992654e+00, 4.94683648e+00,
        4.49980215e+00, 3.56213349e+00, 4.05973768e+00, 5.35244071e+00,
        5.11759606e+00, 5.88058814e+00, 5.48152220e+00, 8.95764021e+00,
        1.27708752e+01, 1.15082836e+01, 1.36037724e+01, 1.74802517e+01,
        2.02171582e+01, 1.73960710e+01, 1.75597377e+01, 2.16283726e+01,
        2.51109892e+01, 2.09876227e+01, 2.43819951e+01, 1.94163093e+01,
        1.00366621e+01, 1.09747464e+01, 1.41332819e+01, 1.64013111e+01,
        1.31233502e+00, 4.26572354e-01, 1.98205538e-01, 1.11854070e-01,
        5.64554009e-02, 3.64083127e-02, 1.83359172e-02, 9.33453486e-03
    };
    public static ReadOnlyCollection<double> PRICE_LIST => _priceList.AsReadOnly();


}