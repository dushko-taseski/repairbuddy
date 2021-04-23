<template>
  <div>
    <button
      class="btn btn-success mr-2 mb-2 w-25"
      @click="
        updateAsset = params.parent.getAsset(params.data);
        params.parent.openUpdateModal();
      "
    >
      <font-awesome-icon icon="edit" />
    </button>
    <button
      class="btn btn-danger mr-2 mb-2 w-25"
      @click="
        deleteAsset = params.parent.getAssetId(params.data.id);
        params.parent.openDeleteModal();
      "
    >
      <font-awesome-icon icon="trash" />
    </button>
  </div>
</template>