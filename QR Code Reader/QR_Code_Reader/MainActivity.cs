using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Gms.Vision.Barcodes;
using Android.Util;

namespace QR_Code_Reader
{
    [Activity(Label = "QR_Code_Reader", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ImageView imageView;
            Button btnScan;
            EditText edttxtResult;

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            btnScan = FindViewById<Button>(Resource.Id.btnScan);
            edttxtResult = FindViewById<EditText>(Resource.Id.txtResult);

            Bitmap bitMap = BitmapFactory.DecodeResource(ApplicationContext.Resources, Resource.Drawable.qrcode);
            imageView.SetImageBitmap(bitMap);

            btnScan.Click += delegate
            {
                BarcodeDetector detector = new BarcodeDetector.Builder(ApplicationContext).SetBarcodeFormats(BarcodeFormat.QrCode).Build();

                Android.Gms.Vision.Frame fram = new Android.Gms.Vision.Frame.Builder().SetBitmap(bitMap).Build();
                SparseArray barsCode = detector.Detect(fram);
                Barcode result = (Barcode)barsCode.ValueAt(0);
                edttxtResult.Text = result.RawValue;
            };
        }
    }
}

