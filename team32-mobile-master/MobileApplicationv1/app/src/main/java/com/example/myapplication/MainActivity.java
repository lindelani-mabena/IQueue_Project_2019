package com.example.myapplication;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;

import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;

import com.google.android.material.navigation.NavigationView;

public class MainActivity extends AppCompatActivity {

    //ListView listView;
    Button button;
    public Members members;

    private DrawerLayout dl;
    private ActionBarDrawerToggle  t;
    private NavigationView nv;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        button = (Button) findViewById(R.id.button);


        button.setOnClickListener(new View.OnClickListener() {
             @Override

             public void onClick(View view) {
                 Intent intent = new Intent(MainActivity.this, ServiceList.class);

                 startActivity(intent);
             }
         });

        dl = (DrawerLayout)findViewById(R.id.activity_main);
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
                    case R.id.update:
                        Toast.makeText(MainActivity.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(MainActivity.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(MainActivity.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(MainActivity.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(MainActivity.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(MainActivity.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }


                return true;

            }
        });




    }



    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if(t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }


  /*  public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.home, menu);
        return true;
    }


  /*  public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        switch (item.getItemId()) {
            case R.id.l:
                Intent intent = new Intent(MainActivity.this, Login.class);

                startActivity(intent);
                //newGame();
                return true;
            case R.id.reg:
                Intent intent2 = new Intent(MainActivity.this, Register.class);

                startActivity(intent2);
                //newGame();
                return true;
            case 0:
                //showHelp();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }*/


}