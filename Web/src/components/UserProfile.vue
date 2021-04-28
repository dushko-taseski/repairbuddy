<template>
  <div>
    <nav class="navbar navbar-inverse bg-dark text-white pt-3 pb-3">
      <div class="col-4 navbar-header">
        <router-link to="/customers"
          ><h4 class="d-inline-block text-white">RepairBuddy</h4></router-link
        >
        <router-link to="/tickets">
          <h4 class="d-inline-block ml-4 text-white hover-shadow">Tickets</h4>
        </router-link>
      </div>
      <div class="col-8 d-flex justify-content-end">
        <h4 class="mr-5">Hello {{ user.fullName }}</h4>
        <router-link to="/user/profile">
          <h4 class="d-inline-block mr-5 text-white">
            <font-awesome-icon icon="user" /><span class="ml-1">Profile</span>
          </h4>
        </router-link>
        <router-link to="/user/login">
          <h4 class="d-inline-block text-white">
            <font-awesome-icon icon="sign-out-alt" /><span
              class="ml-1"
              @click="logoutUser"
              >Log out</span
            >
          </h4>
        </router-link>
      </div>
    </nav>
    <div class="container mt-3 profile">
      <h1 class="mb-5 mt-5 text-center">User Profile</h1>
      <div class="userProfile">
        <div class="row mb-2">
          <div class="col-1">
            <font-awesome-icon icon="envelope" class="envelope" />
          </div>
          <div class="col-11">
            <span class="ml-3">{{ user.email }}</span>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-1">
            <font-awesome-icon icon="user" class="user" />
          </div>
          <div class="col-11">
            <span class="ml-3">{{ user.fullName }}</span>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-1">
            <font-awesome-icon icon="address-book" class="address" />
          </div>
          <div class="col-11">
            <span class="ml-3">{{ user.address }}</span>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-1">
            <font-awesome-icon icon="phone-alt" class="phone" />
          </div>
          <div class="col-11">
            <span class="ml-3">{{ user.phone }}</span>
          </div>
        </div>
      </div>

      <div class="container userButtons">
        <button
          class="btn btn-success updateButton"
          @click="
            openUpdateModal();
            updateUser = user;
          "
        >
          <font-awesome-icon icon="edit" class="edit" />
        </button>
        <button
          class="btn btn-danger ml-2 deleteButton"
          @click="
            openDeleteModal();
            deletingUserProfile = user.id;
          "
        >
          <font-awesome-icon icon="trash" class="delete" />
        </button>
      </div>
    </div>

    <div v-if="updateModal">
      <transition name="model">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Update User</h4>
                  <button
                    type="button"
                    class="close"
                    @click="updateModal = false"
                  >
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="form-group">
                    <label>Full Name</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateUser.fullName"
                    />
                  </div>
                  <div class="form-group">
                    <label>Address</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateUser.address"
                    />
                  </div>
                  <div class="form-group">
                    <label>Phone</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateUser.phone"
                    />
                  </div>
                  <br />
                  <div class="modal-footer" align="right">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="updateModal = false"
                    >
                      Close
                    </button>
                    <button class="btn btn-primary btn-xs" @click="updatedUser">
                      Update
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
    <div v-if="deleteModal">
      <transition name="model">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Delete Asset</h4>
                  <button class="close" @click="deleteModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <h4>Are you want sure delete profile?</h4>
                  </div>
                  <br />
                  <div align="right" class="modal-footer">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="deleteModal = false"
                    >
                      No
                    </button>
                    <button
                      class="btn btn-primary btn-xs"
                      @click="deleteUserProfile"
                    >
                      Yes
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

export default {
  name: "UserProfile",
  data() {
    return {
      deletingUserProfile: null,
      updateModal: false,
      deleteModal: false,
      user: null,
      updateUser: {
        id: "",
        fullName: "",
        address: "",
        phone: "",
      },
    };
  },
  created() {
    this.loadUser();
  },
  methods: {
    openUpdateModal() {
      this.updateModal = true;
    },
    openDeleteModal() {
      this.deleteModal = true;
    },
    loadUser() {
      axios
        .get(`http://localhost:8030/user/profile`, { withCredentials: true })
        .then((response) => {
          this.user = response.data;
        });
    },
    updatedUser() {
      axios
        .post(`http://localhost:8030/user/update`, this.updateUser, {
          withCredentials: true,
        })
        .then((response) => {
          this.updateModal = false;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    deleteUserProfile() {
      axios
        .post(
          `http://localhost:8030/user/delete?userId=${this.deletingUserProfile}`,
          null,
          {
            withCredentials: true,
          }
        )
        .then((response) => {
          console.log(response);
          this.deleteModal = false;
          this.$router.push("/user/login");
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    logoutUser() {
      axios
        .post(`http://localhost:8030/user/logout`, null, {
          withCredentials: true,
        })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error.data);
        });
    },
  },
};
</script>
<style scoped>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.profile {
  border: 1px solid blue;
  width: 700px;
  height: 500px;
}

.userProfile {
  margin-left: 40px;
}

.userButtons {
  margin-left: 30px;
  margin-top: 150px;
}

.envelope,
.user,
.address,
.phone {
  width: 30px;
  height: 30px;
}

.edit,
.delete {
  width: 20px;
  height: 20px;
}
.updateButton,
.deleteButton {
  width: 100px;
}
</style>