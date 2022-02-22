package com.example.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import com.androidnetworking.AndroidNetworking;
import com.androidnetworking.common.Priority;
import com.androidnetworking.error.ANError;
import com.androidnetworking.interfaces.JSONObjectRequestListener;
import com.google.android.material.navigation.NavigationView;

import org.json.JSONObject;

public class Update_Profile extends AppCompatActivity {
    private String Access_Token;
    private Button Update;
    TextView txtFirstName ;
    TextView txtLastName ;
    TextView txtTitle ;
    TextView txtPhoneNumber ;


    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update__profile);
         txtFirstName = (TextView)findViewById(R.id.txtFirstName);
         txtLastName = (TextView)findViewById(R.id.txtLastName);
         txtTitle = (TextView)findViewById(R.id.txtTitle);
         txtPhoneNumber = (TextView)findViewById(R.id.txtPhoneNumber);
        SharedPreferences sharedPreferences = getSharedPreferences("userInfo", Context.MODE_PRIVATE);
        Access_Token = sharedPreferences.getString("token", "");
        Update = (Button)findViewById(R.id.btnUpdate);
        Update.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
               // Access_Token= getIntent().getExtras().getString("Token");

                UpdateProfiles();
            }
        });

        dl = (DrawerLayout)findViewById(R.id.update__profile);
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
                        Toast.makeText(Update_Profile.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(Update_Profile.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(Update_Profile.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(Update_Profile.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(Update_Profile.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(Update_Profile.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(Update_Profile.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(Update_Profile.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });
    }
    public  void UpdateProfiles()
    {
        String Url="http://192.168.137.1:1997/api/Account/Profile/Update";
        AndroidNetworking.post(Url)
                .addHeaders("Content-Type","application/x-www-form-urlencoded;charset=UTF8")
                .addHeaders("Authorization" , "Bearer "+ Access_Token)
                .addUrlEncodeFormBodyParameter("FirstName", txtFirstName.getText().toString())
                .addUrlEncodeFormBodyParameter("LastName", txtLastName.getText().toString())
                .addUrlEncodeFormBodyParameter("Title", txtTitle.getText().toString())
                .addUrlEncodeFormBodyParameter("PhoneNumber", txtPhoneNumber.getText().toString())
                .setPriority(Priority.HIGH)
                .build()
                .getAsJSONObject(new JSONObjectRequestListener() {
                    @Override
                    public void onResponse(JSONObject response) {
                      //  Toast.makeText(getApplicationContext(),response.toString(), Toast.LENGTH_LONG).show();
                        Intent newIntent = new Intent(getApplicationContext(), BActivity.class);
                        startActivity(newIntent);
                    }
                    @Override
                    public void onError(ANError error) {
                        Toast.makeText(getApplicationContext(),error.getMessage(), Toast.LENGTH_SHORT).show();
                        Intent newIntent = new Intent(getApplicationContext(), BActivity.class);
                        startActivity(newIntent);
                    }
                });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }

}
