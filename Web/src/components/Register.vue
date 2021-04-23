<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <h2 class="card-header">Create an account - RepairBuddy</h2>
          <div class="mt-3">
            <span class="text-danger ml-4">{{ errorMessage }}</span>
          </div>
          <div class="card-body">
            <form
              class="form-horizontal"
              @submit.prevent="submitForm"
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
                  errorUserEmail
                }}</span>
              </div>

              <div class="form-group">
                <label for="fullName" class="cols-sm-2 control-label"
                  >Full Name</label
                >
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-envelope fa" aria-hidden="true"></i
                    ></span>
                    <input
                      type="text"
                      class="form-control"
                      id="fullName"
                      placeholder="Enter your FullName"
                      v-model="user.fullName"
                    />
                  </div>
                </div>
                <span v-if="user.fullName == ''" class="text-danger">{{
                  errorUserFullName
                }}</span>
              </div>

              <div class="form-group">
                <label for="address" class="cols-sm-2 control-label"
                  >Address</label
                >
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-users fa" aria-hidden="true"></i
                    ></span>
                    <input
                      type="text"
                      class="form-control"
                      id="address"
                      placeholder="Enter your Address"
                      v-model="user.address"
                    />
                  </div>
                </div>
              </div>
              <div class="form-group">
                <label for="phone" class="cols-sm-2 control-label">Phone</label>
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-users fa" aria-hidden="true"></i
                    ></span>
                    <input
                      type="text"
                      class="form-control"
                      id="phone"
                      placeholder="Enter your Phone"
                      v-model="user.phone"
                    />
                  </div>
                </div>
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
                  errorUserPassword
                }}</span>
              </div>

              <div class="form-group">
                <label for="confirm" class="cols-sm-2 control-label"
                  >Confirm Password</label
                >
                <div class="cols-sm-10">
                  <div class="input-group">
                    <span class="input-group-addon"
                      ><i class="fa fa-lock fa-lg" aria-hidden="true"></i
                    ></span>
                    <input
                      type="password"
                      class="form-control"
                      id="confirm"
                      placeholder="Confirm your Password"
                      v-model="user.confirmPassword"
                    />
                  </div>
                </div>
                <span v-if="user.confirmPassword == ''" class="text-danger">{{
                  errorUserConfirmPassowrd
                }}</span>
              </div>

              <div class="form-group">
                <button
                  type="submit"
                  class="btn btn-primary btn-lg btn-block login-button"
                >
                  Register
                </button>
              </div>
              <p class="loginhere">
                Have already an account ?
                <router-link to="/user/login">Login Here</router-link>
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
  name: "Register",
  data() {
    return {
      user: {
        email: "",
        fullName: "",
        address: "",
        phone: "",
        password: "",
        confirmPassword: "",
      },
      errorUserEmail: null,
      errorUserFullName: null,
      errorUserPassword: null,
      errorUserConfirmPassowrd: null,
      errorMessage: null,
    };
  },

  methods: {
    submitForm() {
      if (
        this.user.email != "" &&
        this.user.fullName != "" &&
        this.user.password != "" &&
        this.user.confirmPassword != ""
      ) {
        axios
          .post(`http://localhost:8030/user/register`, this.user)
          .then((response) => {
            console.log(response);
            this.$router.push("/user/login");
          })
          .catch((error) => {
            this.errorMessage = error.response.data;
          });
      } else if (
        this.user.email == "" &&
        this.user.fullName == "" &&
        this.user.password == "" &&
        this.user.confirmPassword == ""
      ) {
        this.errorUserEmail = "The email is required!";
        this.errorUserFullName = "The full name is required!";
        this.errorUserPassword = "The password is required!";
        this.errorUserConfirmPassowrd = "The confirm password is required!";
      } else if (this.user.email == "") {
        this.errorUserEmail = "The email is required!";
      } else if (this.user.fullName == "") {
        this.errorUserFullName = "The full name is required!";
      } else if (this.user.password == "") {
        this.errorUserPassword = "The password is required!";
      } else if (this.errorUserConfirmPassowrd == "") {
        this.errorUserConfirmPassowrd = "The confirm password is required!";
      }
    },
  },
};
</script>
