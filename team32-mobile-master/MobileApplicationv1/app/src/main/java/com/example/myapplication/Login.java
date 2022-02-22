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
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationView;
import com.jacksonandroidnetworking.JacksonParserFactory;


import org.json.JSONException;
import org.json.JSONObject;

import java.util.concurrent.TimeUnit;


public class Login extends AppCompatActivity {
    Button loginn;
    private String Access_Token;
    private String Access_Tokens;
    TextView _signupLink;
    Members member;

    private DrawerLayout dl;
    private ActionBarDrawerToggle  t;
    private NavigationView nv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login2);
        AndroidNetworking.initialize(getApplicationContext());
        AndroidNetworking.setParserFactory(new JacksonParserFactory());
        final TextView txtUsername = (TextView)findViewById(R.id.username);
        final TextView txtPassword =(TextView)findViewById(R.id.password);
        member = new Members();


        // login bt action listenor
        loginn = (Button) findViewById(R.id.btnLogin);
        _signupLink = (TextView) findViewById(R.id.link_signup);

        loginn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Login(txtUsername.getText().toString(), txtPassword.getText().toString());
                SaveInfo(txtUsername.getText().toString(), txtPassword.getText().toString(), Access_Token);
            }
        });

        _signupLink.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // Start the Signup activity
                Intent intent = new Intent(getApplicationContext(), Register.class);
                startActivity(intent);
            }
        });


        dl = (DrawerLayout)findViewById(R.id.activity_login2);
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
                        Toast.makeText(Login.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(Login.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(Login.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(Login.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(Login.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(Login.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(Login.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(Login.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });
    }
    public  void Login(final String Username, String Password)
    {
        String Url="http://192.168.137.1:1997/token";
        String email =Username;
        final String password=Password;
        String grant_type="password";
        AndroidNetworking.post(Url)
                .addHeaders("Content-Type","application/x-www-form-urlencoded;charset=UTF8")
                .addUrlEncodeFormBodyParameter("Username", email)
                .addUrlEncodeFormBodyParameter("Password", password)
                .addUrlEncodeFormBodyParameter("grant_type", grant_type)
                .setPriority(Priority.HIGH)
                .build()
                .getAsJSONObject(new JSONObjectRequestListener() {
                    @Override
                    public void onResponse(JSONObject response) {
                        // do anything with response

                        try {

                            Access_Token = response.getString("access_token");
                            Access_Tokens =Access_Token;
                            saveAccesToken(Access_Token);
                            member.setAccess_Token(Access_Token);

                           // Toast.makeText(getApplicationContext(),Access_Token, Toast.LENGTH_LONG).show();
                        } catch (JSONException e) {

                        }


                      //  Toast.makeText(getApplicationContext(),response.toString(), Toast.LENGTH_LONG).show();
                        // you will get the access token
                        SaveInfo(Username, password, Access_Token);
                        Intent BranchesIntent= new Intent(getApplicationContext(), Update_Profile.class);
                        BranchesIntent.putExtra("Token", Access_Token);
                        startActivity(BranchesIntent);
                    }
                    @Override
                    public void onError(ANError error) {
                        // handle error
                        // Log.v(TAG, error.getMessage());
                        Toast.makeText(getApplicationContext(),error.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }


    private void SaveInfo(String Username, String Password, String Token)
    {
        SharedPreferences sharedPreferences= getSharedPreferences("userInfo", Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        //editor.putString("username",Username);
        //editor.putString("password", Password);
        editor.putString("token", Access_Token);
       // editor.putLong("ExpiryDate", System.currentTimeMillis()+ TimeUnit.MINUTES.toMillis(1440));
        editor.apply();
    }

    private void saveAccesToken(String accessToken) {
        SharedPreferences sharedPreferences = getSharedPreferences("sharePrefs", MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString("Access Token", accessToken).apply();
    }

    public static String getAccessToken(Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("sharePrefs", MODE_PRIVATE);
        return sharedPreferences.getString("Access Token", "");
    }

}
