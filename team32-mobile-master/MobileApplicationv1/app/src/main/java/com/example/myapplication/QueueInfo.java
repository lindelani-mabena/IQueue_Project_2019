package com.example.myapplication;
        import androidx.annotation.NonNull;
        import androidx.appcompat.app.ActionBarDrawerToggle;
        import androidx.appcompat.app.AlertDialog;
        import androidx.appcompat.app.AppCompatActivity;
        import androidx.core.app.NotificationCompat;
        import androidx.drawerlayout.widget.DrawerLayout;

        import android.app.NotificationChannel;
        import android.app.NotificationManager;
        import android.app.PendingIntent;
        import android.content.Context;
        import android.content.DialogInterface;
        import android.content.Intent;
        import android.content.SharedPreferences;
        import android.graphics.Color;
        import android.os.Build;
        import android.os.Bundle;
        import android.util.Log;
        import android.view.MenuItem;
        import android.view.View;
        import android.widget.ArrayAdapter;
        import android.widget.Button;
        import android.widget.ListView;
        import android.widget.Toast;


        import com.androidnetworking.AndroidNetworking;
        import com.androidnetworking.common.Priority;
        import com.androidnetworking.error.ANError;
        import com.androidnetworking.interfaces.JSONObjectRequestListener;
        import com.google.android.material.bottomnavigation.BottomNavigationView;

        import com.google.android.material.navigation.NavigationView;

        import org.json.JSONException;
        import org.json.JSONObject;

        import java.util.ArrayList;


        import retrofit2.Call;
        import retrofit2.Callback;
        import retrofit2.Response;

public class QueueInfo extends AppCompatActivity {
    int ServID;
    String BranchID;

    ListView listView ;
    Button btnJoin;
    Service service;
    private int TicketSize;
    private String Access_Token;
    private String strToken;
    private String time;


    Members member;


    private NotificationManager notificationManager;
    private static final int NOTIFICATION_ID=0;
    private static final String PRIMARY_CHANNEL_ID = "primary_notification_channel";

    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;
    String token;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        BranchID =getIntent().getExtras().getString("branch");
        member = new Members();
        setContentView(R.layout.activity_queue_info);

         token = Login.getAccessToken(getApplicationContext());
       // Toast.makeText(getApplicationContext(),"This is token "+ token, Toast.LENGTH_LONG).show();
        notificationManager =(NotificationManager)getSystemService(NOTIFICATION_SERVICE);

        ServID= getIntent().getExtras().getInt("servID");
        viewQueue();
       // member.setToken("Ticket " + ServID);
        SharedPreferences sharedPreferences = getSharedPreferences("userInfo", Context.MODE_PRIVATE);
       // Access_Token = sharedPreferences.getString("token", "");
        Access_Token = member.getAccess_Token();


        setContentView(R.layout.activity_queue_info);
        listView = (ListView)findViewById(R.id.listV);
        btnJoin  =(Button)findViewById(R.id.btnJQ);
        viewQueue();

        dl = (DrawerLayout)findViewById(R.id.QI);
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
                        Toast.makeText(QueueInfo.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(QueueInfo.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(QueueInfo.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(QueueInfo.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(QueueInfo.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(QueueInfo.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(QueueInfo.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(QueueInfo.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });



        // bottom nav functionality
        BottomNavigationView bottomNavigationView = (BottomNavigationView) findViewById(R.id.nav_view);


        bottomNavigationView.setOnNavigationItemSelectedListener(
                new
                                                                         BottomNavigationView.OnNavigationItemSelectedListener() {
                                                                             @Override
                                                                             public boolean onNavigationItemSelected(MenuItem item) {
                                                                                 //  Fragment selectedFragment = null;
                                                                                 switch (item.getItemId()) {
                                                                                     case R.id.nav_sev:


                                                                                         Intent intent = new Intent(getApplicationContext(), ServiceList.class);
                                                                                         intent.putExtra("ID", member.getBranchID() + "");
                                                                                         startActivity(intent);

                                                                                         break;
                                                                                     case R.id.nav_join:


                                                                                         Intent intent2 = new Intent(getApplicationContext(), QueueInfo.class);

                                                                                         intent2.putExtra("servID", member.getServeId());
                                                                                         intent2.putExtra("branch",member.getBranchID() );
                                                                                         startActivity(intent2);

                                                                                         break;
                                                                                     case R.id.nav_token:
                                                                                         Intent intent3 = new Intent(QueueInfo.this, QueueInfo.class);

                                                                                         startActivity(intent3);
                                                                                         break;

                                                                                 }

                                                                                 return true;
                                                                             }
                                                                         });


    }

    public void viewQueue()
    {
        NetworkHandler client = NetworkClient.getNetworkClient().create(NetworkHandler.class);
        Call<Service> call = client.getService(ServID);
        service = new Service();

        call.enqueue(new Callback<Service>() {
            @Override
            public void onResponse(Call<Service> call, Response<Service> response) {
               // Toast.makeText(getApplicationContext(), response.toString(), Toast.LENGTH_SHORT).show();
                service = response.body();
                ArrayList<String> Tickets = new ArrayList<>();
                ArrayList<Ticket> TicketList = new ArrayList<>();
                TicketSize= service.Tickets.size();
                for(int i=0; i< service.Tickets.size();i++)
                {

                  if(service.Tickets.get(i).getStatus().equals("Pending")) {
                      TicketList.add(service.Tickets.get(i));
                      Tickets.add(service.Tickets.get(i).getToken());
                  }
                }

                listView.setAdapter(new ArrayAdapter<>(getApplicationContext(),
                        //android.R.layout.nameOfListClass, Services
                        android.R.layout.simple_list_item_1, Tickets));
            }

            @Override
            public void onFailure(Call<Service> call, Throwable t) {
                Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_SHORT).show();

            }
        });

    }
    public void Join(View view)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(QueueInfo.this);
        builder.setTitle("Confirm Join Queue!");
        builder.setMessage("Are you Sure you want to Join this Queue?");
        builder.setCancelable(false);
        builder.setPositiveButton("No", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

                Toast.makeText(getApplicationContext(), "Queue Join Cancelled", Toast.LENGTH_SHORT).show();
            }
        });

        builder.setNegativeButton("Yes", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                deliverNotification(QueueInfo.this);
                NetworkHandler pi = NetworkClient.getNetworkClient().create(NetworkHandler.class);
                String ID = ServID + "";


                if (token == "") {
                    Intent intent = new Intent(getApplicationContext(), Login.class);
                    startActivity(intent);
                } else {

                 join(BranchID, ServID+"");
                /*   pi.joinQueue(ID, BranchID, "Bearer " + token).enqueue(new Callback<Ticket>() {
                        @Override
                        public void onResponse(Call<Ticket> call, Response<Ticket> response) {
                            Log.v("MainActivity", "SUCCESS");
                            Log.v("MainActivity", "Response: " + response.message());
                            Toast.makeText(getApplicationContext(), response.toString(), Toast.LENGTH_LONG).show();
                            viewQueue();
                            Intent intent = new Intent(getApplicationContext(), QueueToken.class);
                            //JSONObject joinedTicket= response.body();
                            try {

                               //  strToken = joinedTicket.getString("Token");
                                //String time = joinedTicket.getString("EstimatedWaitingTime");
                            }
                            catch (Exception ex)
                            {
                            }

                            member.setToken(strToken);

                            //Time newTime = new Time();
                            //new Time=DateHelper.convertToTime(strTime);
                            Toast.makeText(getApplicationContext(), "token is" + response.body(), Toast.LENGTH_LONG).show();

                            startActivity(intent);
                        }
                        @Override
                        public void onFailure(Call<Ticket> call, Throwable t) {
                            Log.v("MainActivity", "ERROR");
                            Log.v("MainActivity", "Error Response: " + call.toString());
                            Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_LONG).show();
                        }
                    });
                    Toast.makeText(getApplicationContext(), "Queue Joined, View Your Queue Token", Toast.LENGTH_SHORT).show(); */
                }

            }
        });

        builder.show();
    }

    public void createNotificationChannel()
    {
        notificationManager =(NotificationManager)getSystemService(NOTIFICATION_SERVICE);


        if(Build.VERSION.SDK_INT>= Build.VERSION_CODES.O)
        {
            NotificationChannel notificationChannel = new NotificationChannel(PRIMARY_CHANNEL_ID,"get to the Bank", NotificationManager.IMPORTANCE_HIGH);

            notificationChannel.enableLights(true);
            notificationChannel.setLightColor(Color.RED);
            notificationChannel.enableVibration(true);
            notificationChannel.setDescription("Notification to get to the bank");
            notificationChannel.getAudioAttributes();
            notificationManager.createNotificationChannel(notificationChannel);
        }

    }

    public void deliverNotification(Context context)
    {
        Intent contentIntent = new Intent(context, QueueInfo.class);

        PendingIntent contentPendingIntent = PendingIntent.getActivity(context, NOTIFICATION_ID,contentIntent,PendingIntent.FLAG_UPDATE_CURRENT);
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, PRIMARY_CHANNEL_ID)
                .setSmallIcon(R.drawable.common_google_signin_btn_icon_dark)
                .setContentTitle("Queue Joined ")
                .setContentText("You have successfully joined a queue")
                .setContentIntent(contentPendingIntent)
                .setPriority(NotificationCompat.PRIORITY_HIGH)
                .setAutoCancel(true)
                .setDefaults(NotificationCompat.DEFAULT_ALL);

        notificationManager.notify(NOTIFICATION_ID,builder.build());
    }

    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }



    public  void join(String Branch_ID, String Serv_ID) {
        if (token == "") {
            Intent intent = new Intent(getApplicationContext(), Login.class);
            startActivity(intent);
        } else {
            String Url = "http://192.168.137.1:1997/api/Services/{id}/Join/Branches/{branchid}/mobile";

            AndroidNetworking.post(Url)
                    .addHeaders("Authorization", "Bearer " + token)
                    .addPathParameter("branchid", Branch_ID)
                    .addPathParameter("id", Serv_ID)
                    .setPriority(Priority.HIGH)
                    .build()
                    .getAsJSONObject(new JSONObjectRequestListener() {
                        @Override
                        public void onResponse(JSONObject response) {
                            // do anything with response
                            try {

                                strToken = response.getString("Token");
                                time = response.getString("EstimatedWaitingTime");



                            } catch (JSONException e) {

                            }

                            member.setToken(strToken);
                            Intent intent = new Intent(getApplicationContext(), QueueToken.class);
                            intent.putExtra("time", time);



                            startActivity(intent);
                        }
                        @Override
                        public void onError(ANError error) {
                            // handle error
                            // Log.v(TAG, error.getMessage());
                            Toast.makeText(getApplicationContext(), error.getMessage(), Toast.LENGTH_SHORT).show();
                        }
                    });
        }
    }

}
