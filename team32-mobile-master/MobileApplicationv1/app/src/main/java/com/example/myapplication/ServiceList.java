package com.example.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationView;

import java.util.ArrayList;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


public class ServiceList extends AppCompatActivity {

    ListView listView;
    private String BranchId;
    Members members;
    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;
    Service servicesList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_service_list);
        listView = (ListView) findViewById(R.id.listView);

        BranchId= getIntent().getExtras().getString("ID");
        getServices(BranchId);

        members = new Members();

        dl = (DrawerLayout)findViewById(R.id.serviceacitivty);
        t = new ActionBarDrawerToggle(this, dl,R.string.app_name,R.string.app_name);

        dl.addDrawerListener(t);
        t.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        nv = (NavigationView)findViewById(R.id.nv);
        nv.setNavigationItemSelectedListener(
                new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id = item.getItemId();
                switch(id)
                {
                    case R.id.home:
                        Toast.makeText(ServiceList.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent6 = new Intent(ServiceList.this, HomeP.class);

                        startActivity(intent6);break;
                    case R.id.update:
                        Toast.makeText(ServiceList.this, "Update Profile",Toast.LENGTH_SHORT).show();
                        Intent intent2 = new Intent(ServiceList.this, Update_Profile.class);

                        startActivity(intent2);break;
                    case R.id.login:
                        Toast.makeText(ServiceList.this, "Login",Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(ServiceList.this, Login.class);

                        startActivity(intent);break;
                    case R.id.register:
                        Toast.makeText(ServiceList.this, "Register",Toast.LENGTH_SHORT).show();
                        Intent intent4 = new Intent(ServiceList.this, Register.class);

                        startActivity(intent4);break;
                    default:
                        return true;
                }
                return true;
            }
        });

        BottomNavigationView bottomNavigationView = (BottomNavigationView) findViewById(R.id.nav_view);


        bottomNavigationView.setOnNavigationItemSelectedListener(new
                                                                         BottomNavigationView.OnNavigationItemSelectedListener() {
                                                                             @Override
                                                                             public boolean onNavigationItemSelected(MenuItem item) {
                                                                                 //  Fragment selectedFragment = null;
                                                                                 switch (item.getItemId()) {
                                                                                     case R.id.nav_branch:
                                                                                         Intent intent = new Intent(ServiceList.this, BranchesMapsActivity.class);

                                                                                         startActivity(intent);
                                                                                         break;
                                                                                     case R.id.nav_sev:
                                                                                         Intent intent2 = new Intent(ServiceList.this, ServiceList.class);

                                                                                         startActivity(intent2);
                                                                                         break;
                                                                                     case R.id.nav_stat:
                                                                                         Intent intent3 = new Intent(ServiceList.this, QueueInfo.class);

                                                                                         startActivity(intent3);
                                                                                         break;

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
    public void getServices(final String BranchId)
    {
        NetworkHandler client = NetworkClient.getNetworkClient().create(NetworkHandler.class);

        Call<Branch> call = client.getBranch(BranchId);

        call.enqueue(new Callback<Branch>() {
            @Override
            public void onResponse(Call<Branch> call, Response<Branch> response) {

                Branch branch = response.body();

                //final  String[] Services = new String[branch.Services.size()];
                ArrayList<String> Services = new ArrayList<>();
                final ArrayList<Service> ServiceList = new ArrayList<>();

                for (int i = 0; i < branch.Services.size(); i++) {
                    //Services[i] = branch.Services.get(i).getName();
                    Services.add(branch.Services.get(i).getName());
                    ServiceList.add(branch.Services.get(i));
                   // servicesList.add(branch.Services)

                }

                // getting the selected branch name and service name
              // members.setBranchName(branch.Services.get(i).getName());
//                members.setBranchName(" BRANCHED");

                listView.setAdapter(new ArrayAdapter<>(getApplicationContext(),
                        //android.R.layout.nameOfListClass, Services
                        android.R.layout.simple_list_item_1, Services));

                listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> adapterView, View view,
                                            int position, long l) {
                        Intent intent = new Intent(ServiceList.this, QueueInfo.class);
                        int temp =ServiceList.get(position).getId();

                        // getting the selected branch name and service name
                    //    String sevn = ServiceList.get(temp).getName();
                        members.setServiceName(ServiceList.get(position).getName());
                        members.setServeId(ServiceList.get(position).getId());

                        String strtemp= temp +"";
                  //      Toast.makeText(getApplicationContext(),strtemp, Toast.LENGTH_LONG).show();
                        intent.putExtra("servID", temp);
                        intent.putExtra("branch",BranchId );
                        startActivity(intent);
                    }
                });
            }

            @Override
            public void onFailure(Call<Branch> call, Throwable t) {

                Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }


}
