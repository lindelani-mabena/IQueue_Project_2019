package com.example.myapplication;

import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.pdf.PdfDocument;
import android.os.Build;
import android.os.Bundle;

import com.example.myapplication.data.DateHelper;
import com.example.myapplication.data.Time;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.app.NotificationCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.os.CountDownTimer;
import android.os.Environment;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Date;
import java.util.List;
import java.util.concurrent.TimeUnit;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class QueueToken extends AppCompatActivity {

    TextView text1;
    Button btnCreate;
    TextView editText;

    private NotificationManager notificationManager;
    private static final int NOTIFICATION_ID=0;
    private static final String PRIMARY_CHANNEL_ID = "primary_notification_channel";

    private static final String FORMAT = "%02d:%02d:%02d";
    Members mem;

    int seconds , minutes ;
    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;
    private String time;
    private int hr;
    private int mins;
    private int secs;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_queue_token);
        Toolbar toolbar = findViewById(R.id.toolbar);

        notificationManager =(NotificationManager)getSystemService(NOTIFICATION_SERVICE);
        time = getIntent().getStringExtra("time");
        Time timer =new Time();
        timer =DateHelper.convertToTime(time);
        hr=timer.getHours()*60*60;
        mins=timer.getMinutes()*60;
        secs=(timer.getSeconds() + hr+mins) *10000;

    // saving to pdf
     /*   btnCreate = (Button)findViewById(R.id.btnSave);
        editText =(TextView) findViewById(R.id.textView4);
        btnCreate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
               // createPdf(editText.getText().toString());
            }
        });
        */


        // displaying the branch and service name of the
        mem = new Members();
        TextView txtsevname = (TextView) findViewById(R.id.txtServiceName);

        TextView txtbranch = (TextView) findViewById(   R.id.txtBranchname);

        TextView txttoken = (TextView) findViewById(R.id.txtToken);
        txtbranch.setText(mem.getBranchName());
        txtsevname.setText(mem.getServiceName());
        txttoken.setText(mem.getToken());


        // displaying counter
        text1=(TextView)findViewById(R.id.textView4);

        new CountDownTimer(secs, 1000) { // adjust the milli seconds here

            public void onTick(long millisUntilFinished) {

                text1.setText(""+String.format(FORMAT,
                        TimeUnit.MILLISECONDS.toHours(millisUntilFinished),
                        TimeUnit.MILLISECONDS.toMinutes(millisUntilFinished) - TimeUnit.HOURS.toMinutes(
                                TimeUnit.MILLISECONDS.toHours(millisUntilFinished)),
                        TimeUnit.MILLISECONDS.toSeconds(millisUntilFinished) - TimeUnit.MINUTES.toSeconds(
                                TimeUnit.MILLISECONDS.toMinutes(millisUntilFinished))));
            }

            public void onFinish() {
                text1.setText("See Consultant!");
                deliverNotification(QueueToken.this);



            }
        }.start();



        dl = (DrawerLayout)findViewById(R.id.QToken);
        t = new ActionBarDrawerToggle(this, dl,R.string.app_name,R.string.app_name);

        dl.addDrawerListener(t);
        t.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        nv = (NavigationView)findViewById(R.id.nv);
        nv.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id = item.getItemId();
                switch(id)
                {
                    case R.id.home:
                        Toast.makeText(QueueToken.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(QueueToken.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(QueueToken.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(QueueToken.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(QueueToken.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(QueueToken.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(QueueToken.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(QueueToken.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });


        // bottom nav functionality
        BottomNavigationView bottomNavigationView = (BottomNavigationView) findViewById(R.id.nav_view);


        bottomNavigationView.setOnNavigationItemSelectedListener(new
                                                                         BottomNavigationView.OnNavigationItemSelectedListener() {
                                                                             @Override
                                                                             public boolean onNavigationItemSelected(MenuItem item) {
                                                                                 //  Fragment selectedFragment = null;
                                                                                 switch (item.getItemId()) {
                                                                                     case R.id.nav_que:
                                                                                         Intent intent = new Intent(QueueToken.this, ServiceList.class);

                                                                                         startActivity(intent);
                                                                                         break;
                                                                                     case R.id.nav_token:
                                                                                         Intent intent2 = new Intent(getApplicationContext(), QueueInfo.class);

                                                                                         intent2.putExtra("servID", mem.getServeId());
                                                                                         intent2.putExtra("branch",mem.getBranchID() );
                                                                                         startActivity(intent2);
                                                                                         break;
                                                                                     case R.id.nav_print:
                                                                                         Intent intent3 = new Intent(QueueToken.this, QueueInfo.class);

                                                                                         startActivity(intent3);
                                                                                         break;

                                                                                 }

                                                                                 return true;
                                                                             }
                                                                         });





    }

    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }

    public void createNotificationChannel()
    {
        notificationManager =(NotificationManager)getSystemService(NOTIFICATION_SERVICE);


        if(Build.VERSION.SDK_INT>= Build.VERSION_CODES.O)
        {
            NotificationChannel notificationChannel = new NotificationChannel(PRIMARY_CHANNEL_ID,"consultation", NotificationManager.IMPORTANCE_HIGH);

            notificationChannel.enableLights(true);
            notificationChannel.setLightColor(Color.RED);
            notificationChannel.enableVibration(true);
            notificationChannel.setDescription("Notification to go and consult");
            notificationChannel.getAudioAttributes();
            notificationManager.createNotificationChannel(notificationChannel);
        }

    }

    public void deliverNotification(Context context)
    {
        Intent contentIntent = new Intent(context, QueueToken.class);

        PendingIntent contentPendingIntent = PendingIntent.getActivity(context, NOTIFICATION_ID,contentIntent,PendingIntent.FLAG_UPDATE_CURRENT);
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, PRIMARY_CHANNEL_ID)
                .setSmallIcon(R.drawable.common_google_signin_btn_icon_dark)
                .setContentTitle("Consult")
                .setContentText("Go to a consultant!")
                .setContentIntent(contentPendingIntent)
                .setPriority(NotificationCompat.PRIORITY_HIGH)
                .setAutoCancel(true)
                .setDefaults(NotificationCompat.DEFAULT_ALL);

        notificationManager.notify(NOTIFICATION_ID,builder.build());
    }


   /* public void CalculateTime()
    {
        NetworkHandler pi = NetworkClient.getNetworkClient().create(NetworkHandler.class);
        pi.getTickets(getIntent().getExtras().getInt("servID")).enqueue(new Callback<List<Ticket>>() {
            @Override
            public void onResponse(Call<List<Ticket>> call, Response<List<Ticket>> response) {
                List<Ticket> TicketList = response.body();
                Toast.makeText(getApplicationContext(),response.toString(), Toast.LENGTH_LONG).show();

                //Creating an String array for the ListView
                String[] Tickets = new String[TicketList.size()];

                for (int i = 0; i < TicketList.size(); i++) {
                    Tickets[i] = TicketList.get(i).getToken();

                }
            }

            @Override
            public void onFailure(Call<List<Ticket>> call, Throwable t) {

            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }


    private void createPdf(String sometext){
        // create a new document
            PdfDocument document = new PdfDocument();
        // crate a page description
        PdfDocument.PageInfo pageInfo = new PdfDocument.PageInfo.Builder(300, 600, 1).create();
        // start a page
        PdfDocument.Page page = document.startPage(pageInfo);
        Canvas canvas = page.getCanvas();
        Paint paint = new Paint();
        paint.setColor(Color.RED);
        canvas.drawCircle(50, 50, 30, paint);
        paint.setColor(Color.BLACK);
        canvas.drawText(sometext, 80, 50, paint);
        //canvas.drawt
        // finish the page
        document.finishPage(page);
// draw text on the graphics object of the page
        // Create Page 2
        pageInfo = new PdfDocument.PageInfo.Builder(300, 600, 2).create();
        page = document.startPage(pageInfo);
        canvas = page.getCanvas();
        paint = new Paint();
        paint.setColor(Color.BLUE);
        canvas.drawCircle(100, 100, 100, paint);
        document.finishPage(page);
        // write the document content
        String directory_path = Environment.getExternalStorageDirectory().getPath() + "/mypdf/";
        File file = new File(directory_path);
        if (!file.exists()) {
            file.mkdirs();
        }
        String targetPdf = directory_path+"test-2.pdf";
        File filePath = new File(targetPdf);
        try {
            document.writeTo(new FileOutputStream(filePath));
            Toast.makeText(this, "Done", Toast.LENGTH_LONG).show();
        } catch (IOException e) {
            Log.e("main", "error "+e.toString());
            Toast.makeText(this, "Something wrong: " + e.toString(),  Toast.LENGTH_LONG).show();
        }
        // close the document
        document.close();
    }
    */



}
