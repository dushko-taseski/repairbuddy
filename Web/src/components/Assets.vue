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
    <div style="margin-left: 100px">
      <button class="btn btn-dark mt-3 mb-3" @click="openAddModal">
        <font-awesome-icon icon="plus" class="mr-2" />New Asset
      </button>
    </div>
    <ag-grid-vue
      class="ag-theme-alpine"
      :columnDefs="columnDefs"
      :rowData="assetsForCustomer"
      :pagination="true"
      :paginationPageSize="8"
      :defaultColDef="defaultColDef"
    >
    </ag-grid-vue>
    <div v-if="addModal">
      <transition name="model">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Add New Asset</h4>
                  <button type="button" class="close" @click="addModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <span class="mb-3 text-danger">{{ errorMessage }}</span>
                  <div class="form-group">
                    <label>SerialNumber</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the serial number"
                      v-model="newAsset.serialNumber"
                    />
                    <span
                      class="text-danger"
                      v-if="newAsset.serialNumber == ''"
                      >{{ errorSerialNumber }}</span
                    >
                  </div>
                  <div class="form-group">
                    <label>Name</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the name"
                      v-model="newAsset.name"
                    />
                    <span class="text-danger" v-if="newAsset.name == ''">{{
                      errorName
                    }}</span>
                  </div>
                  <div class="form-group">
                    <label>Description</label>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Enter the description"
                      v-model="newAsset.description"
                    />
                  </div>
                  <br />
                  <div class="modal-footer" align="right">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="addModal = false"
                    >
                      Close
                    </button>
                    <button class="btn btn-primary btn-xs" @click="addNewAsset">
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
                  <h4 class="modal-title">Update Asset</h4>
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
                    <label>SerialNumber</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateAsset.serialNumber"
                    />
                  </div>
                  <div class="form-group">
                    <label>Name</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateAsset.name"
                    />
                  </div>
                  <div class="form-group">
                    <label>Description</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="updateAsset.description"
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
                    <button
                      class="btn btn-primary btn-xs"
                      @click="updateAssetForCustomer"
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
                  <h4 class="modal-title">Delete Asset</h4>
                  <button class="close" @click="deleteModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <h4>Are you want sure delete asset?</h4>
                  </div>
                  <br />
                  <div align="right" class="modal-footer">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="deleteModal = false"
                    >
                      No
                    </button>
                    <button class="btn btn-primary btn-xs" @click="deleteAsset">
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
import assetsActionButtons from "./gridComponents/customerGridComponents/assetsActionButtons";

export default {
  name: "Assets",
  data() {
    return {
      columnDefs: null,
      assetsForCustomer: null,
      clickedCustomerId: null,
      deletingAssetId: null,
      addModal: false,
      updateModal: false,
      deleteModal: false,
      user: null,
      newAsset: {
        serialNumber: "",
        name: "",
        description: "",
        customerId: "",
      },
      updateAsset: {
        id: "",
        serialNumber: "",
        name: "",
        description: "",
      },
      errorSerialNumber: null,
      errorName: null,
      errorMessage: null,
    };
  },
  components: {
    AgGridVue,
    assetsActionButtons,
  },
  created() {
    this.clickedCustomerId = this.$route.params.id;
    this.loadAssetsForCustomer();
    this.loadUser();
  },
  methods: {
    openAddModal() {
      this.addModal = true;
    },
    openUpdateModal() {
      this.updateModal = true;
    },
    openDeleteModal() {
      this.deleteModal = true;
    },
    loadAssetsForCustomer() {
      axios
        .get(
          `http://localhost:8030/assets?customerId=${this.clickedCustomerId}`,
          {
            withCredentials: true,
          }
        )
        .then(({ data }) => {
          this.assetsForCustomer = data;
        });
    },
    loadUser() {
      axios
        .get(`http://localhost:8030/user/profile`, { withCredentials: true })
        .then((response) => {
          this.user = response.data;
        });
    },
    addNewAsset() {
      if (this.newAsset.serialNumber != "" && this.newAsset.name != "") {
        axios
          .post(
            `http://localhost:8030/asset/new?customerId=${this.clickedCustomerId}`,
            this.newAsset,
            {
              withCredentials: true,
            }
          )
          .then((response) => {
            console.log(response);
            this.addModal = false;
            this.loadAssetsForCustomer();
            this.newAsset.serialNumber = "";
            this.newAsset.name = "";
            this.newAsset.description = "";
            this.errorMessage = null;
          })
          .catch((error) => {
            this.errorMessage = error.response.data;
          });
      } else if (this.newAsset.serialNumber == "" && this.newAsset.name == "") {
        this.errorSerialNumber = "The serial number is required!";
        this.errorName = "The name is required!";
      } else if (this.newAsset.serialNumber == "") {
        this.errorSerialNumber = "The serial number is required!";
      } else if (this.newAsset.name == "") {
        this.errorName = "The name is required!";
      }
    },
    updateAssetForCustomer() {
      axios
        .post(`http://localhost:8030/asset/update`, this.updateAsset, {
          withCredentials: true,
        })
        .then((response) => {
          console.log(response);
          this.updateModal = false;
          this.loadAssetsForCustomer();
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    deleteAsset() {
      if (this.deletingAssetId > 0) {
        axios
          .post(
            `http://localhost:8030/asset/delete?assetId=${this.deletingAssetId}`,
            null,
            { withCredentials: true }
          )
          .then((response) => {
            console.log(response);
            this.deleteModal = false;
            this.loadAssetsForCustomer();
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
    getAsset(asset) {
      this.updateAsset = asset;
    },
    getAssetId(assetId) {
      this.deletingAssetId = assetId;
    },
  },
  beforeMount() {
    this.columnDefs = [
      { field: "serialNumber", sortable: true, filter: true },
      { field: "name", sortable: true, filter: true },
      { field: "description", sortable: true },
      {
        headerName: "Actions",
        cellRendererFramework: "assetsActionButtons",
        cellRendererParams: {
          parent: this,
        },
      },
    ];
    this.defaultColDef = {
      flex: 1,
    };
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
  height: 500px;
  margin-left: 100px;
  margin-right: 100px;
}
</style>