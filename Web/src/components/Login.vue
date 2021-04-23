<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <h2 class="card-header">Log In - RepairBuddy</h2>
          <div class="ml-3 mt-3">
            <span class="text-danger">{{ errorMessage }}</span>
          </div>
          <div class="card-body">
            <form
              class="form-horizontal"
              @submit.prevent="loginForm"
              method="post"
            >
              <div class="form-group">
                <label for="email" class="cols-sm-2 control-label">Email</label>
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-user fa" aria-hidden="true"></i
                    ></span>
                    <input
                      type="text"
                      class="form-control"
                      id="email"
                      placeholder="Enter your Email"
                      v-model="user.email"
                    />
                  </div>
                </div>
                <span v-if="user.email == ''" class="text-danger">{{
                  errorEmail
                }}</span>
              </div>
              <div class="form-group">
                <label for="password" class="cols-sm-2 control-label"
                  >Password</label
                >
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-lock fa-lg" aria-hidden="true"></i
                    ></span>
                    <input
                      type="password"
                      class="form-control"
                      id="password"
                      placeholder="Enter your Password"
                      v-model="user.password"
                    />
                  </div>
                </div>
                <span v-if="user.password == ''" class="text-danger">{{
                  errorPassword
                }}</span>
              </div>
              <div class="form-group">
                <button
                  type="submit"
                  class="btn btn-primary btn-lg btn-block login-button"
                >
                  Login
                </button>
              </div>
              <p class="loginhere">
                Don't have an account?
                <router-link to="/user/register">Register Here</router-link>
              </p>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

export default {
  name: "Login",
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
      errorEmail: null,
      errorPassword: null,
      errorMessage: null,
    };
  },

  methods: {
    loginForm() {
      if (this.user.email != "" && this.user.password != "") {
        axios
          .post(`http://localhost:8030/user/login`, this.user, {
            withCredentials: true,
          })
          .then((response) => {
            console.log(response);
            this.$store.commit("setAuthentication", true);
            this.$router.push("/customers");
          })
          .catch((error) => {
            this.errorMessage = error.response.data;
          });
      } else if (this.user.email == "" && this.user.password == "") {
        this.errorEmail = "The email is required!";
        this.errorPassword = "The password is required!";
      } else if (this.user.email == "") {
        this.errorEmail = "The email is required!";
      } else if (this.user.password == "") {
        this.errorPassword = "The password is required!";
      }
    },
  },
};
</script>