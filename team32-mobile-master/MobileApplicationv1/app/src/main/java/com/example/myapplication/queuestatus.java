package com.example.myapplication;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;

import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.navigation.NavigationView;
import com.google.android.material.snackbar.Snackbar;


import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;

import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.http.Query;

public class queuestatus extends AppCompatActivity {

    Button btnJoin;
    AlertDialog alertDialog;
    ListView listVi;

    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        listVi = (ListView) findViewById(R.id.listView);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_queuestatus);
        Toolbar toolbar = findViewById(R.id.toolbar);

        btnJoin = (Button) findViewById(R.id.btnJoin);

        dl = (DrawerLayout)findViewById(R.id.queustatus);
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
                        Toast.makeText(queuestatus.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(queuestatus.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(queuestatus.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(queuestatus.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(queuestatus.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(queuestatus.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(queuestatus.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(queuestatus.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });





        BottomNavigationView bottomNavigationView;
        bottomNavigationView = (BottomNavigationView) findViewById(R.id.nav_view);


        bottomNavigationView.setOnNavigationItemSelectedListener(new
                                                                         BottomNavigationView.OnNavigationItemSelectedListener() {
                                                                             @Override
                                                                             public boolean onNavigationItemSelected(MenuItem item) {
                                                                                 //  Fragment selectedFragment = null;
                                                                                 switch (item.getItemId()) {
                                                                                     case R.id.nav_join:
                                                                                         Intent intent = new Intent(queuestatus.this, queuestatus.class);

                                                                                         startActivity(intent);
                                                                                         break;
                                                                                     case R.id.nav_sev:
                                                                                         Intent intent2 = new Intent(queuestatus.this, ServiceList.class);

                                                                                         startActivity(intent2);
                                                                                         break;
                                                                                     case R.id.nav_stat:
                                                                                         Intent intent3 = new Intent(queuestatus.this, queuestatus.class);

                                                                                         startActivity(intent3);
                                                                                         break;

                                                                                 }

                                                                                 return true;
                                                                             }
                                                                         });


        btnJoin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {




                AlertDialog.Builder builder = new AlertDialog.Builder(queuestatus.this);
                builder.setTitle("Confirm Join Queue !");
                builder.setMessage("Are you Sure you want to Join this Queue ?");
                builder.setCancelable(false);
                builder.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        Toast.makeText(getApplicationContext(), "Que Join Cancelled", Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(queuestatus.this, QueueToken.class);

                        startActivity(intent);
                    }
                });

                builder.setNegativeButton("No", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        Toast.makeText(getApplicationContext(), "Que Joined, View Your Queue Token", Toast.LENGTH_SHORT).show();
                    }
                });

                builder.show();
            }
        });
    }

    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }

}


