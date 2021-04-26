<template>
  <div>
    <nav class="navbar navbar-inverse bg-dark text-white pt-3 pb-3">
      <div class="col-4 navbar-header">
        <router-link to="/customers"
          ><h4 class="d-inline-block text-white">RepairBuddy</h4></router-link
        >
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
            <font-awesome-icon icon="sign-out-alt" /><span class="ml-1"
              >Log out</span
            >
          </h4>
        </router-link>
      </div>
    </nav>
    <div style="margin-left: 100px">
      <button class="btn btn-dark mt-3 mb-3" @click="openAddModal">
        <font-awesome-icon icon="plus" class="mr-2" />New Ticket
      </button>
    </div>
    <span></span>
    <ag-grid-vue
      class="ag-theme-alpine"
      :columnDefs="columnDefs"
      :rowData="tickets"
      :pagination="true"
      :paginationPageSize="8"
      :getRowHeight="getRowHeight"
      :isFullWidthCell="isFullWidthCell"
      :fullWidthCellRenderer="fullWidthCellRenderer"
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
                  <h4 class="modal-title">Add New Tickets</h4>
                  <button type="button" class="close" @click="addModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <select
                    id="select1"
                    v-model="selected"
                    class="custom-select mb-3"
                    @change="loadAssetsForCustomer"
                  >
                    <option value="">Choose the customer name</option>
                    <option
                      v-for="cust in customers"
                      :key="cust.id"
                      :value="cust.id"
                    >
                      {{ cust.name }}
                    </option>
                  </select>
                  <div id="el">
                    <select
                      id="select2"
                      class="custom-select"
                      :disabled="selected == 0"
                      v-model="newTicket.assetId"
                    >
                      <option value="">Choose the asset</option>
                      <option
                        v-for="asset in assetsForCustomer"
                        :key="asset.id"
                        :value="asset.id"
                      >
                        {{ asset.serialNumber }}
                      </option>
                    </select>
                  </div>
                  <div class="ml-1 mt-3 form-group">
                    <label>Description</label>
                    <input
                      v-model="newTicket.description"
                      type="text"
                      class="form-control"
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
                    <button
                      class="btn btn-primary btn-xs"
                      @click="addNewTicket"
                    >
                      Create
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
    <div v-if="ticketDetailsModal">
      <transition name="model">
        <div class="modal-mask modal-dialog-scrollable">
          <div class="modal-wrapper">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <h4 class="modal-title">Ticket Details</h4>
                  <button class="close" @click="ticketDetailsModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <hr style="border: 1px solid black" />
                    <p><b>Customer Information</b></p>
                    <div class="row">
                      <div class="col-4">
                        <span>Name: </span>
                      </div>
                      <div class="col-8">
                        <span>{{ clickedTicket.asset.customer.name }}</span>
                      </div>
                    </div>
                    <div class="row mt-2">
                      <div class="col-4 mt-2">
                        <span>Email: </span>
                      </div>
                      <div class="col-8 mt-2">
                        <span>{{ clickedTicket.asset.customer.email }}</span>
                      </div>
                    </div>
                  </div>
                  <hr style="border: 1px solid black" />
                  <div>
                    <p><b>Asset Information</b></p>
                    <div class="row">
                      <div class="col-4">
                        <span>Serial Number: </span>
                      </div>
                      <div class="col-8">
                        <span>{{ clickedTicket.asset.serialNumber }}</span>
                      </div>
                    </div>
                  </div>
                  <hr style="border: 1px solid black" />
                  <div>
                    <p><b>Ticket Information</b></p>
                    <div class="row">
                      <div class="col-4">
                        <span>Description: </span>
                      </div>
                      <div class="col-8">
                        <span>{{ clickedTicket.description }}</span>
                      </div>
                    </div>
                    <div class="row mt-2">
                      <div class="col-4 mt-2">
                        <span>Open date: </span>
                      </div>
                      <div class="col-8 mt-2">
                        <span>{{ clickedTicket.dateOpen }}</span>
                      </div>
                    </div>
                    <div class="row mt-2">
                      <div class="col-4 mt-2">
                        <span>Close date: </span>
                      </div>
                      <div class="col-8 mt-2">
                        <span>{{ clickedTicket.dateClosed }}</span>
                      </div>
                    </div>
                    <div class="row mt-2">
                      <div class="col-4 mt-2">
                        <span>Ticket Status </span>
                      </div>
                      <div class="col-8 mt-2">
                        <span>{{ clickedTicket.ticketStatus }}</span>
                      </div>
                    </div>
                  </div>
                  <hr style="border: 1px solid black" />
                  <div>
                    <p><b>New Ticket Item</b></p>
                    <div>
                      <div class="row">
                        <div class="col-6">Description</div>
                        <div class="col-3">Price</div>
                      </div>
                      <div
                        class="row mt-2 mb-2"
                        v-for="ticketItem in ticketItems"
                        :key="ticketItem.id"
                      >
                        <li class="col-6">
                          {{ ticketItem.description }}
                        </li>
                        <div class="col-3">
                          {{ ticketItem.price }}
                        </div>
                        <div class="col-3">
                          <button
                            class="btn btn-danger"
                            v-if="clickedTicket.ticketStatus == 'Open'"
                            @click="
                              clickedTicketItem = ticketItem.id;
                              deleteTicketItem();
                            "
                          >
                            <font-awesome-icon icon="trash" class="w-25" />
                          </button>
                        </div>
                      </div>
                      <span
                        ><b>TotalPrice: </b>
                        {{ clickedTicket.totalPrice }}</span
                      >
                    </div>
                    <div
                      class="row mt-3"
                      v-if="clickedTicket.ticketStatus == 'Open'"
                    >
                      <div class="col-5">
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Add a description"
                          v-model="newTicketItem.description"
                        />
                      </div>
                      <div class="col-4">
                        <input
                          type="number"
                          class="form-control"
                          placeholder="Add a price"
                          v-model="newTicketItem.price"
                        />
                      </div>
                      <div class="col-3">
                        <button
                          class="btn btn-success w-100"
                          @click="addNewTicketItem"
                        >
                          <font-awesome-icon icon="check" />
                        </button>
                      </div>
                    </div>
                  </div>
                  <hr style="border: 1px solid black" />
                  <br />
                  <div align="right" class="modal-footer">
                    <button
                      class="btn btn-secondary btn-xs mr-2"
                      @click="ticketDetailsModal = false"
                    >
                      Close
                    </button>
                    <button
                      class="btn btn-primary btn-xs"
                      v-if="clickedTicket.ticketStatus == 0"
                      @click="closeTicket"
                    >
                      Close Ticket
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
                  <h4 class="modal-title">Delete Ticket</h4>
                  <button class="close" @click="deleteModal = false">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div>
                    <h4>Are you want sure delete ticket?</h4>
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
                      @click="deleteTicket"
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
import ticketsActionButtons from "./gridComponents/customerGridComponents/ticketsActionButtons";

export default {
  name: "Tickets",
  data() {
    return {
      addModal: false,
      deleteModal: false,
      ticketDetailsModal: false,
      deletingTicketId: null,
      customers: null,
      assetsForCustomer: null,
      tickets: null,
      ticketItems: null,
      clickedTicket: null,
      clickedTicketItem: null,
      user: null,
      errorMessage: null,
      selected: "",
      newTicket: {
        description: "",
        dateOpen: "",
        ticketStatus: "",
        dateClosed: "",
        assetId: "",
      },
      newTicketItem: {
        description: "",
        price: "",
        ticketId: "",
      },
    };
  },

  components: {
    AgGridVue,
    ticketsActionButtons,
  },

  mounted() {
    axios
      .get(`http://localhost:8030/customers`, { withCredentials: true })
      .then((response) => {
        this.customers = response.data;
      });
  },

  created() {
    this.loadTickets();
    this.loadUser();
  },

  methods: {
    openAddModal() {
      this.addModal = true;
    },
    openDeleteModal() {
      this.deleteModal = true;
    },
    openTicketDetailsModal() {
      this.ticketDetailsModal = true;
    },
    getTicketId(ticketId) {
      this.deletingTicketId = ticketId;
    },
    getTicket(ticket) {
      this.clickedTicket = ticket;
    },
    loadTickets() {
      axios
        .get(`http://localhost:8030/tickets`, { withCredentials: true })
        .then((response) => {
          console.log(response.data);
          this.tickets = response.data;
        });
    },
    loadUser() {
      axios
        .get(`http://localhost:8030/user/profile`, { withCredentials: true })
        .then((response) => {
          this.user = response.data;
        });
    },
    loadTicketItems() {
      axios
        .get(
          `http://localhost:8030/ticket/ticketItems?ticketId=${this.clickedTicket.id}`,
          {
            withCredentials: true,
          }
        )
        .then((response) => {
          this.ticketItems = response.data;
          console.log(this.test);
        });
    },
    loadAssetsForCustomer() {
      axios
        .get(`http://localhost:8030/assets?customerId=${this.selected}`, {
          withCredentials: true,
        })
        .then((response) => {
          this.assetsForCustomer = response.data;
        });
    },
    addNewTicket() {
      axios
        .post(`http://localhost:8030/ticket/new`, this.newTicket, {
          withCredentials: true,
        })
        .then((response) => {
          this.addModal = false;
          this.loadTickets();
          console.log(response);
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    addNewTicketItem() {
      this.newTicketItem.price = Number(this.newTicketItem.price);
      this.newTicketItem.ticketId = this.clickedTicket.id;
      axios
        .post(
          `http://localhost:8030/ticket/newTicketItem`,
          this.newTicketItem,
          {
            withCredentials: true,
          }
        )
        .then((response) => {
          this.loadTickets();
          this.loadTicketItems();
          this.newTicketItem.description = "";
          this.newTicketItem.price = "";
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    closeTicket() {
      axios
        .post(
          `http://localhost:8030/ticket/closeTicket?ticketId=${this.clickedTicket.id}`,
          null,
          { withCredentials: true }
        )
        .then((response) => {
          this.ticketDetailsModal = false;
          this.loadTickets();
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    },
    deleteTicket() {
      if (this.deletingTicketId > 0) {
        axios
          .post(
            `http://localhost:8030/ticket/delete?ticketId=${this.deletingTicketId}`,
            null,
            { withCredentials: true }
          )
          .then((response) => {
            this.deleteModal = false;
            this.loadTickets();
            console.log(response.data);
          })
          .catch((error) => {
            console.log(error.response.data);
          });
      }
    },
    deleteTicketItem() {
      axios
        .post(
          `http://localhost:8030/ticket/deleteTicketItem?ticketItemId=${this.clickedTicketItem}`,
          null,
          { withCredentials: true }
        )
        .then((response) => {
          this.loadTicketItems();
          this.loadTickets();
          console.log(response.data);
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
  beforeMount() {
    this.columnDefs = [
      {
        field: "name",
        valueGetter: (params) => {
          return params.data.asset.customer.name;
        },
        sortable: true,
        filter: true,
      },
      {
        field: "email",
        valueGetter: (params) => {
          return params.data.asset.customer.email;
        },
        sortable: true,
        filter: true,
      },
      {
        field: "serialNumber",
        valueGetter: (params) => {
          return params.data.asset.serialNumber;
        },
        sortable: true,
      },
      { field: "description", sortable: true },
      {
        field: "dateOpen",
        headerName: "Open date",
        sortable: true,
      },
      { field: "ticketStatus", sortable: true },
      {
        field: "dateClosed",
        headerName: "Closed date",
        sortable: true,
      },
      {
        headerName: "Actions",
        cellRendererFramework: "ticketsActionButtons",
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