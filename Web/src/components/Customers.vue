<template>
  <div>
    <nav class="navbar navbar-dark bg-primary row">
      <div class="col-6">
        <h4 class="d-inline-block">RepairBuddy</h4>
      </div>
      <div class="col-4 d-flex justify-content-end">
        <h4>Hello {{ user.fullName }}</h4>
      </div>
      <div
        class="col-1 btn-group"
        role="group"
        aria-label="Button group with nested dropdown"
      >
        <div class="btn-group" role="group">
          <button
            id="btnGroupDrop1"
            type="button"
            class="btn btn-primary dropdown-toggle"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Dropdown
          </button>
          <ul class="dropdown-menu" aria-labelledby="btnGroupDrop1">
            <li><a class="dropdown-item" href="#">Dropdown link</a></li>
            <li><a class="dropdown-item" href="#">Dropdown link</a></li>
          </ul>
        </div>
      </div>
      <!-- <div class="col-1">
        <div class="btn-group d-flex justify-content-end w-100">
          <button
            type="button"
            class="btn btn-secondary dropdown-toggle"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Account
          </button>
          <div class="dropdown-menu">
            <router-link to="/user/profile">
              <span class="btn d-block w-100">Profile</span>
            </router-link>
            <router-link to="/user/login">
              <span @click="logoutUser" class="btn w-100 d-block mt-1">
                Log Out
              </span>
            </router-link>
          </div>
        </div>
      </div> -->
    </nav>
    <div class="row">
      <div class="col-6">
        <button class="btn btn-secondary mt-3 mb-3 ml-3" @click="openAddModal">
          Add New Customer
        </button>
        <router-link to="/tickets">
          <button class="btn btn-secondary mt-3 mb-3 ml-3 d-inline-block">
            Tickets
          </button>
        </router-link>
      </div>
    </div>
    <ag-grid-vue
      class="ag-theme-alpine"
      :columnDefs="columnDefs"
      :rowData="customers"
      :pagination="true"
      :paginationPageSize="8"
    >
    </ag-grid-vue>
    <div v-if="addModal">
      <transition name="model">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Add New Customer</h4>
                  <button type="button" class="close" @click="addModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <span class="text-danger mb-3"> {{ errorEmail }}</span>
                  <div class="form-group">
                    <label>Name</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the name"
                      v-model="newCustomer.name"
                    />
                    <span class="text-danger" v-if="newCustomer.name == ''">
                      {{ errorCustomerName }}
                    </span>
                  </div>
                  <div class="form-group">
                    <label>Email</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the email"
                      v-model="newCustomer.email"
                    />
                    <span class="text-danger" v-if="newCustomer.email == ''">
                      {{ errorCustomerEmail }}
                    </span>
                  </div>
                  <div class="form-group">
                    <label>Address</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the address"
                      v-model="newCustomer.address"
                    />
                  </div>
                  <div class="form-group">
                    <label>City</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the city"
                      v-model="newCustomer.city"
                    />
                  </div>
                  <div class="form-group">
                    <label>Phone</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the phone"
                      v-model="newCustomer.phone"
                    />
                  </div>
                  <br />
                  <div align="right" class="modal-footer">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="addModal = false"
                    >
                      Close
                    </button>
                    <button class="btn btn-primary btn-xs" @click="addCustomer">
                      Save
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
    <div v-if="updateModal">
      <transition name="model">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Update Customer</h4>
                  <button class="close" @click="updateModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="form-group">
                    <label>Name</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the name"
                      v-model="updateCustomer.name"
                    />
                  </div>
                  <div class="form-group">
                    <label>Address</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the address"
                      v-model="updateCustomer.address"
                    />
                  </div>
                  <div class="form-group">
                    <label>City</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the city"
                      v-model="updateCustomer.city"
                    />
                  </div>
                  <div class="form-group">
                    <label>Phone</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the phone"
                      v-model="updateCustomer.phone"
                    />
                  </div>
                  <br />
                  <div align="right" class="modal-footer">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="updateModal = false"
                    >
                      Close
                    </button>
                    <button
                      class="btn btn-primary btn-xs"
                      @click="updatedCustomer"
                    >
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
                  <h4 class="modal-title">Delete Customer</h4>
                  <button class="close" @click="deleteModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <h4>Are you want sure delete customer?</h4>
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
                      @click="deleteCustomer"
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
import { AgGridVue } from "ag-grid-vue";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-alpine.css";
import actionButtons from "./gridComponents/customerGridComponents/actionButtons";

export default {
  name: "Customers",
  data() {
    return {
      columnDefs: null,
      customers: null,
      deletingCustomerId: null,
      addModal: false,
      deleteModal: false,
      updateModal: false,
      errorCustomerName: null,
      errorCustomerEmail: null,
      errorEmail: null,
      user: null,
      updateCustomer: {
        id: "",
        name: "",
        address: "",
        city: "",
        phone: "",
      },
      newCustomer: {
        name: "",
        email: "",
        address: "",
        city: "",
        phone: "",
        userId: "",
      },
    };
  },

  components: {
    AgGridVue,
    actionButtons,
  },
  created() {
    this.loadCustomer();
    this.loadUser();
  },
  methods: {
    openAddModal() {
      this.addModal = true;
    },
    openDeleteModal() {
      this.deleteModal = true;
    },
    openUpdateModal() {
      this.updateModal = true;
    },
    loadCustomer() {
      axios
        .get(`http://localhost:8030/customers`, { withCredentials: true })
        .then(({ data }) => {
          this.customers = data;
        });
    },
    addCustomer() {
      if (this.newCustomer.name != "" && this.newCustomer.email != "") {
        axios
          .post(`http://localhost:8030/customer/new`, this.newCustomer, {
            withCredentials: true,
          })
          .then((response) => {
            console.log(response);
            this.addModal = false;
            this.loadCustomer();
            this.newCustomer.name = "";
            this.newCustomer.email = "";
            this.newCustomer.address = "";
            this.newCustomer.phone = "";
            this.newCustomer.city = "";
          })
          .catch((error) => {
            this.errorEmail = error.response.data;
          });
      } else if (this.newCustomer.name == "" && this.newCustomer.email == "") {
        this.errorCustomerName = "The customer name is required!";
        this.errorCustomerEmail = "The customer email is required!";
      } else if (this.newCustomer.name == "") {
        this.errorCustomerName = "The customer name is required";
      } else if (this.newCustomer.email == "") {
        this.errorCustomerEmail = "The customer email is required!";
      }
    },
    loadUser() {
      axios
        .get(`http://localhost:8030/user/profile`, { withCredentials: true })
        .then((response) => {
          this.user = response.data;
        });
    },
    updatedCustomer() {
      axios
        .post(`http://localhost:8030/customer/update`, this.updateCustomer, {
          withCredentials: true,
        })
        .then((response) => {
          this.updateModal = false;
          this.loadCustomer();
          console.log(response);
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    deleteCustomer() {
      if (this.deletingCustomerId > 0) {
        axios
          .post(
            `http://localhost:8030/customer/delete?customerId=${this.deletingCustomerId}`,
            null,
            { withCredentials: true }
          )
          .then((response) => {
            this.deleteModal = false;
            this.loadCustomer();
            console.log(response);
          })
          .catch((error) => {
            console.log(error.response.data);
          });
      }
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
    getCustomerId(customerId) {
      this.deletingCustomerId = customerId;
    },
    getCustomer(customer) {
      this.updateCustomer = customer;
    },
  },
  beforeMount() {
    this.columnDefs = [
      { field: "name", sortable: true, filter: true },
      { field: "email", sortable: true, filter: true, minWidth: 280 },
      { field: "address", sortable: true },
      { field: "city", sortable: true },
      { field: "phone" },
      {
        headerName: "Actions",
        cellRendererFramework: "actionButtons",
        cellRendererParams: {
          parent: this,
        },
        minWidth: 270,
      },
    ];
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

.ag-theme-alpine {
  width: 100%;
  height: 500px;
}
</style>