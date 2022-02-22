package com.example.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationView;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Register extends AppCompatActivity {

    String strUsername;
    String strPassword;
    String strConfirmPassword;
    TextView log ;

    private DrawerLayout dl;
    private ActionBarDrawerToggle  t;
    private NavigationView nv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        log = (TextView) findViewById(R.id.link_signup);

        log.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Start the Signup activity
                Intent intent = new Intent(getApplicationContext(), Login.class);
                startActivity(intent);
            }
        });


        dl = (DrawerLayout)findViewById(R.id.activity_register);
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
                        Toast.makeText(Register.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(Register.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(Register.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(Register.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(Register.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(Register.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(Register.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(Register.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });


    }
    public void Register(View view)
    {
        AddUser();
    }
    private void AddUser()
    {

        TextView Username  = (TextView)findViewById(R.id.txtEmail);
        strUsername = Username.getText().toString();

        TextView Password =(TextView) findViewById(R.id.txtPassword);
        strPassword =Password.getText().toString();

        TextView ConfirmPassword = (TextView) findViewById(R.id.txtConfirmPassword);
        strConfirmPassword =ConfirmPassword.getText().toString();
        NetworkHandler pi = NetworkClient.getNetworkClient().create(NetworkHandler.class);

        Client client = new Client(strUsername, strPassword, strConfirmPassword);
        pi.Register(client).enqueue(new Callback<Client>() {
            @Override
            public void onResponse(Call<Client> call, Response<Client> response) {
                Log.v("MainActivity", "SUCCESS");
                Log.v("MainActivity", "Response: " + response.message());
                Toast.makeText(getApplicationContext(),response.toString(), Toast.LENGTH_LONG).show();
                Intent intent6 = new Intent(Register.this, Login.class);

                startActivity(intent6);
            }
            @Override
            public void onFailure(Call<Client> call, Throwable t) {
                Log.v("MainActivity", "ERROR");
                Log.v("MainActivity", "Error Response: " + call.toString());
                Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
        Toolbar toolbar = findViewById(R.id.toolbar);

    }



    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }



}
