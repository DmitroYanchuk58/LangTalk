package com.example.langtalkmobileapp
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val emailBox = findViewById<EditText>(R.id.Email)
        val passwordBox = findViewById<EditText>(R.id.Password)
        val loginButton = findViewById<Button>(R.id.LoginButton)

        loginButton.setOnClickListener {
            val email = emailBox.text.toString()
            val password = passwordBox.text.toString()

            CoroutineScope(Dispatchers.IO).launch {
                try {
                    val client = OkHttpClient.Builder()
                        .hostnameVerifier { _, _ -> true } // Ignore SSL errors (for localhost)
                        .build()

                    val url = "https://10.0.2.2:7067/Auth/Login?email=$email&password=$password"
                    val request = Request.Builder().url(url).build()

                    val response = client.newCall(request).execute()

                    runOnUiThread {
                        Toast.makeText(this@MainActivity, "Status: ${response.code}", Toast.LENGTH_SHORT).show()
                    }

                } catch (ex: Exception) {
                    runOnUiThread {
                        Toast.makeText(this@MainActivity, "Error: ${ex.message}", Toast.LENGTH_LONG).show()
                    }
                }
            }
        }
    }
}
