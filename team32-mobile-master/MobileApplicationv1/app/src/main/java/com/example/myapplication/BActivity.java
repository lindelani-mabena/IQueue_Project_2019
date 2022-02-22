package com.example.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.cardview.widget.CardView;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import android.widget.Toast;


import com.google.android.material.navigation.NavigationView;

import org.w3c.dom.Text;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class BActivity extends AppCompatActivity {

    Members member;

    ListView listView;
    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_b);

        listView = (ListView) findViewById(R.id.listBranches);
        Branches();

        member = new Members();

        dl = (DrawerLayout) findViewById(R.id.bactivity);
        t = new ActionBarDrawerToggle(this, dl, R.string.app_name, R.string.app_name);

        dl.addDrawerListener(t);
        t.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        nv = (NavigationView) findViewById(R.id.nv);
        nv.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id = item.getItemId();
                switch (id) {
                    case R.id.home:
                        Toast.makeText(BActivity.this, "Update Profile", Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(BActivity.this, HomeP.class);

                        startActivity(intent6);
                        break;
                    case R.id.update:
                        Toast.makeText(BActivity.this, "Update Profile", Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(BActivity.this, Update_Profile.class);

                        startActivity(intent2);
                        break;
                    case R.id.login:
                        Toast.makeText(BActivity.this, "Login", Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(BActivity.this, Login.class);

                        startActivity(intent);
                        break;
                    case R.id.register:
                        Toast.makeText(BActivity.this, "Register", Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(BActivity.this, Register.class);

                        startActivity(intent4);
                        break;
                    default:
                        return true;
                }


                return true;

            }
        });

    }


    private void Branches() {


        NetworkHandler pi = NetworkClient.getNetworkClient().create(NetworkHandler.class);

        Call<List<Branch>> call = pi.getBranches();

        call.enqueue(new Callback<List<Branch>>() {
            @Override
            public void onResponse(Call<List<Branch>> call, Response<List<Branch>> response) {


                final List<Branch> BranchList = response.body();
                Toast.makeText(getApplicationContext(), response.toString(), Toast.LENGTH_SHORT).show();

                //Creating a String array for the ListView
                String[] Branches = new String[BranchList.size()];
                Integer[] BranchIDs = new Integer[BranchList.size()];


                for (int i = 0; i < BranchList.size(); i++) {
                    Branches[i] = BranchList.get(i).getName();
                }

                listView.setAdapter(new ArrayAdapter<String>(getApplicationContext(), android.R.layout.simple_list_item_1, Branches));
                listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                        Intent intent = new Intent(BActivity.this, ServiceList.class);

                        int temp = BranchList.get(position).getId();

                        member.setBranchName(BranchList.get(position).getName());


                        intent.putExtra("ID", temp + "");
                        startActivity(intent);
                    }
                });
            }

            @Override
            public void onFailure(Call<List<Branch>> call, Throwable t) {
                Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        if (t.onOptionsItemSelected(item))
            return true;

        return super.onOptionsItemSelected(item);
    }

    /*

        cv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(BActivity.this, ServiceList.class);

                startActivity(intent);
            }
        });

    }


}


     */
}
