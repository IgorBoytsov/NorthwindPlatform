
plugins {
    alias(libs.plugins.android.library)
    alias(libs.plugins.kotlin.android)
    alias(libs.plugins.kotlin.compose)
}

android {
    namespace = "com.northwind.authentication"
    compileSdk = 36

    defaultConfig {
        minSdk = 31

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
        consumerProguardFiles("consumer-rules.pro")
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_11
        targetCompatibility = JavaVersion.VERSION_11
    }
    kotlinOptions {
        jvmTarget = "11"
    }

    buildFeatures {
        compose = true
    }
    composeOptions {
        kotlinCompilerExtensionVersion = "1.5.1"
    }
}

dependencies {

    implementation(libs.androidx.core.ktx)
//    implementation(libs.androidx.appcompat)
//    implementation(libs.material)
    testImplementation(libs.junit)
    androidTestImplementation(libs.androidx.junit)
    androidTestImplementation(libs.androidx.espresso.core)

    // Зависимости для Jetpack Compose
    implementation(platform(libs.androidx.compose.bom)) // BoM для управления версиями
    implementation(libs.androidx.activity.compose)      // Для интеграции с Activity (setContent)
    implementation(libs.androidx.compose.ui)             // Ядро UI
    implementation(libs.androidx.compose.ui.graphics)    // Графика
    implementation(libs.androidx.compose.material3)      // Компоненты Material Design 3
    implementation(libs.androidx.compose.ui.tooling.preview) // Для превью в Android Studio
    debugImplementation(libs.androidx.compose.ui.tooling)    // Для инструментов (Layout Inspector)
}